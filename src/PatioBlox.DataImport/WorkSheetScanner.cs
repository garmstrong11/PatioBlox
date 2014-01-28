namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Domain;
	using FlexCel.XlsAdapter;

	public class WorkSheetScanner
	{
		private readonly XlsFile _xlsFile;
		private readonly Domain.Properties.Settings _defs;
		
		public WorkSheetScanner(string xlPath)
		{
			_xlsFile = new XlsFile(xlPath);
			_defs = Domain.Properties.Settings.Default;
		}

		public int FindHeaderRow(int sheetNumber)
		{
			var startColumnIndex = _defs.SearchColumnIndex;
			_xlsFile.ActiveSheet = sheetNumber;

			for (var row = 1; row <= _xlsFile.RowCount; row++) {
				var val = _xlsFile.GetCellValue(row, startColumnIndex) as string;
				if (val == null) continue;
				return row;
			}

			// Throw if no string found
			throw new InvalidDataException(
				string.Format(
					"Can't find header row on sheet {0} of workbook {1}", 
					_xlsFile.SheetName, 
					Path.GetFileName(_xlsFile.ActiveFileName))
			);
		}

		public ColumnIndexes FindColumns(int sheetNum, int searchRow)
		{
			_xlsFile.ActiveSheet = sheetNum;
			var cols = new ColumnIndexes();
			var colCount = _xlsFile.ColCountInRow(searchRow);

			// Start with blank string in index 0 to align with 1 based excel indices:
			var headerTexts = new List<string> {" "};

			for (var col = 1; col <= colCount; col++) {
				var colText = _xlsFile.GetCellValue(searchRow, col) as string;
				if (colText == null) {
					headerTexts.Add(String.Empty);
					continue;
				}

				headerTexts.Add(colText);

				cols.Section = headerTexts.FindIndex(m => m == _defs.SectionColumnName);
				cols.Item = headerTexts.FindIndex(m => m == _defs.ItemColumnName);
				cols.Description = headerTexts.FindIndex(m => m == _defs.DescriptionColumnName);
				cols.PalletQty = headerTexts.FindIndex(m => m == _defs.PalletQtyColumnName);
				cols.Barcode = headerTexts.FindIndex(m => m == _defs.BarcodeColumnName);
			}

			var valueDict = cols.GetValueDict();

			if (valueDict.ContainsValue(-1)) {
				throw new InvalidDataException(
					string.Format("Unable to find the {0} column in worksheet {1} of workbook {2}",
						valueDict.First(b => b.Value == -1).Key, 
						_xlsFile.SheetName, 
						Path.GetFileName(_xlsFile.ActiveFileName))
					);
			}

			return cols;
		}

		public List<Section> FindSections(
			int sheetIndex, int sectionColumnIndex, int headerRow, ref int counter)
		{
			_xlsFile.ActiveSheet = sheetIndex;
			var sections = new List<Section>();

			for (var row = headerRow + 1; row <= _xlsFile.RowCount; row++) {
				var val = _xlsFile.GetCellValue(row, sectionColumnIndex) as string;
				if (val == null) continue;
				if (Regex.IsMatch(val, @"^Page ?\d+")) continue;

				counter++;
				sections.Add(new Section {Id = counter, Name = val, RowNumber = row});
			}

			return sections;
		}

		public List<Patch> FindPatches(ref int counter)
		{
			var patches = new List<Patch>();

			for (var sheet = 1; sheet <= _xlsFile.SheetCount; sheet++) {
				counter++;
				_xlsFile.ActiveSheet = sheet;
				patches.Add(new Patch {Name = _xlsFile.SheetName, Id = counter});
			}

			return patches;
		}

		public List<Product> FindBloxForPatch(int patchId, int headerRow, ColumnIndexes cols, ref int counter)
		{
			var blokList = new List<Product>();

			_xlsFile.ActiveSheet = patchId;

			for (var row = headerRow; row <= _xlsFile.RowCount; row++) {
				var item = _xlsFile.GetCellValue(row, cols.Item) as Double?;

				if (item == null) continue;

				var blok = new Product();

				blok.ItemNumber = Convert.ToInt32(item);

				var desc = _xlsFile.GetCellValue(row, cols.Description) as string;
				blok.Description = desc ?? string.Empty;

				var pq = _xlsFile.GetCellValue(row, cols.PalletQty) as Double?;
				blok.PalletQuantity = pq == null ? string.Empty : pq.ToString();

				var barcode = _xlsFile.GetCellValue(row, cols.Barcode) as Double?;
				blok.Barcode = barcode == null ? new Barcode(String.Empty) : new Barcode(barcode.ToString());

				blok.Id = ++counter;
				blok.Index = row;
				blok.PatchId = patchId;
				//blok.PatchName = _xlsFile.SheetName;

				blokList.Add(blok);
			}

			return blokList;
		} 
	}
}