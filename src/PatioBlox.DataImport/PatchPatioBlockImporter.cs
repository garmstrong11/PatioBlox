namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Domain;

	public class PatchPatioBlockImporter : PatioBlockImporter
	{
		private readonly StoreListImporter _stores;

		public PatchPatioBlockImporter(List<string> filePaths, StoreListImporter stores) 
			: base(filePaths)
		{
			_stores = stores;
		}

		public List<Patch> ImportPatches()
		{
			var resultList = new List<Patch>();
			var patchCount = 0;
			var sectionId = 0;

			foreach (var xl in XlsFiles) {
				for (var sheet = 1; sheet <= xl.SheetCount; sheet++) {
					xl.ActiveSheet = sheet;
					var patchName = xl.SheetName;

					var patch = new Patch
						{
						Name = patchName,
						Id = patchCount++
						};

					int storeCount;
					var hasCount = _stores.PatchStoreCounts.TryGetValue(patchName, out storeCount);
					if (hasCount) patch.StoreCount = storeCount;

					var rowCount = xl.RowCount;
					var startRow = 1;
					string sectionName = "Unknown Section";

					for (var i = 1; i <= rowCount; i++)
					{
						var anchorCandidate = xl.GetCellValue(i, 5);
						if (anchorCandidate == null) continue;

						if ((string)anchorCandidate != "Item #") continue;

						for (var j = i + 1; j <= rowCount; j++)
						{
							var chekVal = xl.GetCellValue(j, 5);

							if (chekVal == null) continue;
							if (!Regex.IsMatch(chekVal.ToString(), @"^[\d\.]+$")) continue;

							startRow = j;

							// Find foirst section name:
							var secCell = xl.GetCellValue(j, 2);
							if (secCell == null || secCell.ToString().Contains("Page")) {
								secCell = xl.GetCellValue(j + 1, 2);
							}
							sectionName = secCell.ToString();

							break;
						}

						break;
					}

					//var row = 15;
					var row = startRow;
					//var val = xl.GetCellValue(row, 2);
					//var sectionName = val != null ? xl.GetCellValue(row, 2).ToString() : "Unknown Section";
					var sectionIndex = 0;

					var section = new Section
						{
						Id = sectionId++,
						Index = sectionIndex++,
						Name = TitleCase(sectionName),
						Patch = patch,
						PatchId = patch.Id
						};

					patch.Sections.Add(section);

					var sequence = 0;
					for (; row <= rowCount; row++) {
						var itemNo = xl.GetCellValue(row, 5);

						if (itemNo == null) continue;

						var val = xl.GetCellValue(row + 1, 2);  // Check for new section
						if (val != null) {
							var nym = val.ToString();
							if (!nym.Contains("Page") && nym != sectionName) {
								sectionName = nym;
								section = new Section
									{
									Id = sectionId++,
									Index = sectionIndex++,
									Name = TitleCase(sectionName),
									Patch = patch,
									PatchId = patch.Id
									};
								patch.Sections.Add(section);
							}
						}

						var block = new PatioBlock
							{
							Id = string.Format("{0}_{1}", patchName, row),
							PatchName = patchName,
							ItemNumber = Convert.ToInt32(itemNo),
							Section = section,
							SectionId = section.Id,
							Index = sequence++
							};

						val = xl.GetCellValue(row, 9);
						block.Barcode = val != null ? new Barcode(val.ToString()) : new Barcode(String.Empty);

						val = xl.GetCellValue(row, 8);
						block.PalletQuantity = val != null ? val.ToString() : string.Empty;

						val = xl.GetCellValue(row, 7);
						block.Description = val != null ? val.ToString() : string.Empty;

						section.PatioBlocks.Add(block);
					}

					resultList.Add(patch);
				}
			}
			return resultList;
		}

		private static string TitleCase(string s)
		{
			return string.IsNullOrWhiteSpace(s) ? "" : new string(CharsToTitleCase(s).ToArray());
		}

		private static IEnumerable<char> CharsToTitleCase(string s)
		{
			var wordSeparators = new List<char> { ' ', '/', '\'' };
			var newWord = true;
			foreach (var c in s)
			{
				if (newWord) { yield return Char.ToUpper(c); newWord = false; }
				else yield return Char.ToLower(c);
				if (wordSeparators.Contains(c)) newWord = true;
			}
		}
	}
}