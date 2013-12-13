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
		protected readonly List<XlsFile> XlsFiles;

		public PatioBlockImporter(List<string> filePaths)
		{
			XlsFiles = new List<XlsFile>();
			filePaths.ForEach(f => XlsFiles.Add(new XlsFile(f, false)));
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

			foreach (var xl in XlsFiles) {
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
						block.Barcode = val != null ? new Barcode(val.ToString()) : new Barcode(String.Empty);
							
						val = xl.GetCellValue(row, 8);
						block.PalletQuantity = val != null ? val.ToString() : string.Empty;

						val = xl.GetCellValue(row, 7);
						block.Description = val != null ? val.ToString() : string.Empty;

						//block.ApprovalStatus = ApprovalStatus.Pending;

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

		public List<PatioBlock> BarcodeViolations
		{
			get
			{
				return DistinctPatioBlocks
					.Where(b => !b.Barcode.IsValid)
					.ToList();
			}
		}

		public List<string> BlockAppearsOnPatches(PatioBlock block)
		{
			return PatioBlocks
				.Where(b => b.Barcode.Equals(block.Barcode)
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
				return XlsFiles.Sum(xlsFile => xlsFile.SheetCount);
			}
		}
	}
}