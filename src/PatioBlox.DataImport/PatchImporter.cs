namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Text.RegularExpressions;
	using Domain;

	public class PatchImporter : Importer
	{
		public PatchImporter(List<string> filePaths) : base(filePaths)
		{
		}

		public List<Patch> ImportPatches()
		{
			var resultList = new List<Patch>();

			foreach (var xl in _xlsFiles) {
				for (var sheet = 1; sheet <= xl.SheetCount; sheet++) {
					int patchCount = 0;
					var patchName = xl.SheetName;

					xl.ActiveSheet = sheet;
					var patch = new Patch
						{
						Name = patchName,
						Id = patchCount++,
						Sections = new List<Section>()
						};

					var rowCount = xl.RowCount;
					var row = 15;
					var val = xl.GetCellValue(row, 2);
					var sectionCount = 0;

					var section = new Section
						{
						Id = sectionCount,
						Index = sectionCount++,
						Name = val != null ? xl.GetCellValue(row, 2).ToString() : "UnknownSection"
						};

					patch.Sections.Add(section);

					for (; row <= rowCount; row++) {
						var itemNo = xl.GetCellValue(row, 5);

						if (itemNo == null) continue;

						val = xl.GetCellValue(row + 1, 2);
						if (val != null) {
							if (!val.ToString().Contains("Page")) {
								section = new Section
									{
									Id = sectionCount,
									Index = sectionCount++,
									Name = val.ToString()
									};
							}
						}

						var block = new PatioBlock
							{
							Id = string.Format("{0}_{1}", patchName, row),
							Patch = patchName,
							ItemNumber = Convert.ToInt32(itemNo)
							};

						val = xl.GetCellValue(row, 9);
						block.Barcode = val != null ? val.ToString() : "Barcode missing!";

						val = xl.GetCellValue(row, 8);
						block.PalletQuantity = val != null ? val.ToString() : string.Empty;

						val = xl.GetCellValue(row, 7);
						block.Description = val != null ? val.ToString() : string.Empty;

						section.PatioBlocks.Add(block);

						resultList.Add(patch);
					}
				}
				return resultList;
			}
		}
	}
}