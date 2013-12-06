﻿namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Domain;

	public class OneUpImporter : PatioBlockImporter
	{
		public OneUpImporter(List<string> filePaths) : base(filePaths)
		{
		}

		public Dictionary<string, OneUpPatioBlock> BlockDict
		{
			get
			{
				var blox = new List<OneUpPatioBlock>();

				foreach (var xlsFile in _xlsFiles) {
					for (var row = 2; row < xlsFile.RowCount; row++) {
						var val = xlsFile.GetCellValue(row, 1);
						var blok = new OneUpPatioBlock();

						if(val == null) continue;

						blok.ItemNumber = Convert.ToInt32(val);

						val = xlsFile.GetCellValue(row, 2);
						blok.Description = val != null ? val.ToString() : "";

						val = xlsFile.GetCellValue(row, 3);
						blok.Name = val != null ? val.ToString() : "";

						val = xlsFile.GetCellValue(row, 4);
						blok.Size = val != null ? val.ToString() : "";

						val = xlsFile.GetCellValue(row, 5);
						blok.Color = val != null ? val.ToString() : "";

						val = xlsFile.GetCellValue(row, 6);
						blok.PalletQuantity = val != null ? val.ToString() : "";

						val = xlsFile.GetCellValue(row, 7);
						blok.Barcode = val != null ? new Barcode(val.ToString()) : new Barcode("");

						val = xlsFile.GetCellValue(row, 8);
						blok.Image = val != null ? val.ToString() : "";

						val = xlsFile.GetCellValue(row, 9);

						if (val != null) {
							var str = val.ToString();
							blok.ApprovalStatus = (ApprovalStatus) Enum.Parse(typeof (ApprovalStatus), str, true);
						}
						else {
							blok.ApprovalStatus = ApprovalStatus.Pending;
						}

						blox.Add(blok);
					}
				}

				return blox.ToDictionary( k => String.Format("{0}_{1}", k.ItemNumber, k.Barcode.ToString()));
			}
		}
	}
}