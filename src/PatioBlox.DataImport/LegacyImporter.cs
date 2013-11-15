﻿namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using Domain;

	public class LegacyImporter : Importer
	{
		public LegacyImporter(List<string> filePaths) : base(filePaths)
		{
		}

		protected override List<PatioBlock> ImportPatioBlox()
		{
			var blox = new List<PatioBlock>();

			foreach (var xlsFile in _xlsFiles) {
				xlsFile.ActiveSheet = 1;
				for (var row = 2; row <= xlsFile.RowCount; row++) {
					var blok = new PatioBlock();

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
					blok.Barcode = val != null ? val.ToString() : string.Empty;

					val = xlsFile.GetCellValue(row, 21);
					blok.Image = val != null ? val.ToString() : string.Empty;

					blox.Add(blok);
				}
			}

			return blox;
		}
	}
}