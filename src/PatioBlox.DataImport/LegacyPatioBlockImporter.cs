namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using Domain;

	public class LegacyPatioBlockImporter : PatioBlockImporter
	{
		public LegacyPatioBlockImporter(List<string> filePaths) : base(filePaths)
		{
		}

		public List<LegacyPatioBlock> ImportLegacyPatioBlox()
		{
			var blox = new List<LegacyPatioBlock>();

			foreach (var xlsFile in XlsFiles) {
				xlsFile.ActiveSheet = 1;
				for (var row = 2; row <= xlsFile.RowCount; row++) {
					var blok = new LegacyPatioBlock();

					var val = xlsFile.GetCellValue(row, 5);
					blok.ItemNumber = val != null ? Convert.ToInt32(val) : 0;

					val = xlsFile.GetCellValue(row, 7);
					blok.Description = val != null ? val.ToString() : string.Empty;

					val = xlsFile.GetCellValue(row, 8);
					blok.Name = val != null ? val.ToString() : string.Empty;

					val = xlsFile.GetCellValue(row, 9);
					blok.Size = val != null ? val.ToString() : string.Empty;

					val = xlsFile.GetCellValue(row, 10);
					blok.Color = val != null ? val.ToString() : string.Empty;

					val = xlsFile.GetCellValue(row, 11);
					blok.PalletQuantity = val != null ? val.ToString() : string.Empty;

					val = xlsFile.GetCellValue(row, 12);
					blok.Barcode = val != null ? new Barcode(val.ToString()) : new Barcode(string.Empty);

					val = xlsFile.GetCellValue(row, 21);
					blok.Image = val != null ? val.ToString() : string.Empty;

					blox.Add(blok);
				}
			}

			return blox;
		}
	}
}