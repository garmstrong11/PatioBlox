namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Domain;
	using FlexCel.XlsAdapter;

	public class Importer
	{
		private readonly List<XlsFile> _xlsFiles;

		public Importer(List<string> filePaths)
		{
			_xlsFiles = new List<XlsFile>();
			filePaths.ForEach(f => _xlsFiles.Add(new XlsFile(f, false)));
			PatioBlocks = ImportPatioBlox();
		}

		public List<PatioBlock> PatioBlocks { get; private set; }

		private List<PatioBlock> ImportPatioBlox()
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
						block.Barcode = val != null ? val.ToString() : "Barcode missing!";
							
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

		public int SheetCount
		{
			get
			{
				return _xlsFiles.Sum(xlsFile => xlsFile.SheetCount);
			}
		}
	}
}