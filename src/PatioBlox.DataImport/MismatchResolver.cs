namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Comparers;
	using Domain;
	using FlexCel.XlsAdapter;

	public class MismatchResolver
	{
		private readonly XlsFile _xls;
		
		public MismatchResolver()
		{
			_xls = new XlsFile();
		}

		public string FactoryDataPath { get; set; }

		public IList<PatchLocator> GetPatchLocators(IList<string> fileNames)
		{
			if(string.IsNullOrWhiteSpace(FactoryDataPath)) throw new InvalidOperationException("FactoryDataPath is not set");
			var result = new List<PatchLocator>();

			foreach (var fileName in fileNames) {
				var fullPath = Path.Combine(FactoryDataPath, fileName);
				_xls.Open(fullPath);

				for (var tab = 1; tab <= _xls.SheetCount; tab++) {
					_xls.ActiveSheet = tab;
					result.Add(new PatchLocator(fileName, _xls.ActiveSheetByName, tab));
				}
			}

			return result;
		}

		public void ResolveMismatches(IList<Corrector> correctors)
		{
			var groupedCorrectors = correctors.GroupBy(c => c.Filename);

			foreach (var correctorGroup in groupedCorrectors) {
				var fullPath = Path.Combine(FactoryDataPath, correctorGroup.Key);
				_xls.Open(fullPath);
				foreach (var corrector in correctorGroup) {
					_xls.SetCellValue(corrector.PatchIndex, corrector.RowIndex, Corrector.ItemIdIndex, corrector.ItemId, -1);
					_xls.SetCellValue(corrector.PatchIndex, corrector.RowIndex, Corrector.DescriptionIndex, corrector.Description, -1);
					_xls.SetCellValue(corrector.PatchIndex, corrector.RowIndex, Corrector.PalletIndex, corrector.PalletQty, -1);
					_xls.SetCellValue(corrector.PatchIndex, corrector.RowIndex, Corrector.UpcIndex, corrector.UPC, -1);
				}

				var nym = string.Format("{0}_Resolved.xlsx", Path.GetFileNameWithoutExtension(correctorGroup.Key));
				var newName = Path.Combine(FactoryDataPath, nym);
				_xls.AllowOverwritingFiles = true;
				_xls.Save(newName);
			}
		}

		public IList<Corrector> GetCorrectors(string fileName, IList<PatchLocator> locators)
		{
			var result = new List<Corrector>();
			var correctorPath = Path.Combine(FactoryDataPath, fileName);
			var locatorDict = locators.ToDictionary(k => k.PatchName);

			_xls.Open(correctorPath);
			_xls.ActiveSheet = 1;
			var rowCount = _xls.RowCount;

			for (var row = 2; row <= rowCount; row++) {
				var itemId = Convert.ToDouble(_xls.GetCellValue(row, 1));
				var desc = _xls.GetCellValue(row, 2).ToString();
				var palletQty = Convert.ToDouble(_xls.GetCellValue(row, 3));
				var upc = _xls.GetCellValue(row, 4).ToString();
				var itemList = _xls.GetCellValue(row, 5).ToString()
					.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)
					.ToList();

				foreach (var item in itemList) {
					var split = item.Split(new[] {"_"}, StringSplitOptions.RemoveEmptyEntries);
					var locator = locatorDict[split[0]];
					var corrector = new Corrector
						{
						ItemId = itemId,
						Description = desc,
						PalletQty = palletQty,
						PatchName = split[0],
						PatchIndex = locator.PatchIndex,
						RowIndex = int.Parse(split[1]),
						Filename = locator.FileName,
						UPC = upc
						};

					result.Add(corrector);
				}
			}

			return result;
		}
		
		public static List<OneUpPatioBlock> ResolveMismatches(List<OneUpPatioBlock> blox)
		{
			var bloxToRemove = new List<OneUpPatioBlock>();

			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 54329 && b.PalletQuantity == "84"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 460716 && b.PalletQuantity == "56"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 80097 && b.Description == "FULTON 4INX11.5IN WALNUT WALL BLK"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548616 && b.Description == "16X6 COUNTRY MANOR RETAINING WALL"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548924 && b.Description == "A+R LUXORA 3-IN Color TBD"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548951 && b.Description == "A+R LUXORA 3-IN H CM CAP"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548958 && b.Description == "A+R LUXORA 16-IN CM WALL"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548987 && b.Description == "A+R LUXORA 6-IN TAN/GRAY CM WALL"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 549001 && b.Description == "A+R INSIGNIA 4INX12IN COLOR TBD"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 549002 && b.Description == "A+R INSIGNIA 2.5-IN H COLOR TBD"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 550897 && b.Description == "A+R INSIGNIA 2.3-IN H  CAP"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 551097 && b.Description == "A+R INSIGNIA 4INX12IN WALL"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 551846 && b.Description == "A+R LUXORA 16-IN COLOR TBD"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 552110 && b.Description == "4 X 12  ALAMEDA CUMBERLAND WALL"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 552294 && b.Description == "4 X12 ALMEDA CUMBERLAND WALL CAP"));
			//bloxToRemove.AddRange(blox.Where(b => b.ItemNumber ==  && b.Description == ""));

			return blox.Except(bloxToRemove, new AllPropertiesOneUpEqualityComparer()).ToList();
		} 
	}
}