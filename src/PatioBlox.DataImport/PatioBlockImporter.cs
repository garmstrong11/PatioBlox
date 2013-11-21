namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Comparers;
	using Domain;
	using FlexCel.XlsAdapter;

	public class PatioBlockImporter
	{
		protected readonly List<XlsFile> _xlsFiles;

		public PatioBlockImporter(List<string> filePaths)
		{
			_xlsFiles = new List<XlsFile>();
			filePaths.ForEach(f => _xlsFiles.Add(new XlsFile(f, false)));
		}

		public List<PatioBlock> PatioBlocks
		{
			get
			{
				return ImportPatioBlox();
			}
		}

		protected virtual List<PatioBlock> ImportPatioBlox()
		{
			var resultList = new List<PatioBlock>();

			foreach (var xl in _xlsFiles) {
				for (var sheet = 1; sheet <= xl.SheetCount; sheet++) {
					xl.ActiveSheet = sheet;
					var patchName = xl.SheetName;

					var rowCount = xl.RowCount;
					for (var row = 15; row <= rowCount; row++) {
						var val = xl.GetCellValue(row, 5);

						if (val == null) continue;

						var block = new PatioBlock
							{
							Id = string.Format("{0}_{1}", patchName, row),
							Patch = patchName,
							ItemNumber = Convert.ToInt32(val)
							};

						val = xl.GetCellValue(row, 9);
						block.Barcode = val != null ? val.ToString() : String.Empty;
							
						val = xl.GetCellValue(row, 8);
						block.PalletQuantity = val != null ? val.ToString() : string.Empty;

						val = xl.GetCellValue(row, 7);
						block.Description = val != null ? val.ToString() : string.Empty;

						resultList.Add(block);
					}
				}
			}

			return resultList;
		}

		public List<PatioBlock> DistinctPatioBlocks
		{
			get { return PatioBlocks.Distinct(new ItemBarcodeEqualityComparer()).ToList(); }
		}

		public List<PatioBlock> ItemBarcodeViolations
		{
			get
			{
				var allPropertiesBlox = PatioBlocks.Distinct(new AllPropertiesPatioBlockEqualityComparer()).ToList();
				var violatorItems = allPropertiesBlox
					.Except(DistinctPatioBlocks, new AllPropertiesPatioBlockEqualityComparer())
					.Select(b => b.ItemNumber)
					.ToList();

				return allPropertiesBlox
					.Where(b => violatorItems.Contains(b.ItemNumber))
					.OrderBy(b => b.ItemNumber)
					.ToList();
			}
		}

		public List<string> BlockAppearsOnPatches(PatioBlock block)
		{
			return PatioBlocks
				.Where(b => b.Barcode == block.Barcode 
					&& b.Description == block.Description
					&& b.ItemNumber == block.ItemNumber
					&& b.PalletQuantity == block.PalletQuantity)
				.OrderBy(b => b.Id)
				.Select(b => b.Id)
				.ToList();
		} 

		public int SheetCount
		{
			get
			{
				return _xlsFiles.Sum(xlsFile => xlsFile.SheetCount);
			}
		}
	}
}