namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Comparers;
	using Domain;
	using FlexCel.XlsAdapter;

	public class MismatchResolver
	{
		private readonly IList<string> _filePaths;
		private readonly string _correctorPath;
		private readonly XlsFile _xls;
		
		public MismatchResolver(IList<string> filePaths, string correctorPath)
		{
			if (filePaths == null) throw new ArgumentNullException("filePaths");
			if (filePaths.Any(string.IsNullOrWhiteSpace)) throw new ArgumentException("Empty paths encountered");
			if (string.IsNullOrWhiteSpace(correctorPath)) throw new ArgumentNullException("correctorPath");

			_filePaths = filePaths;
			_correctorPath = correctorPath;
			_xls = new XlsFile();
		}

		public Dictionary<string, string> GetPatchNameDict()
		{
			var result = new Dictionary<string, string>();

			foreach (var filePath in _filePaths) {
				_xls.Open(filePath);

				for (var tab = 1; tab <= _xls.SheetCount; tab++) {
					_xls.ActiveSheet = tab;
					result.Add(_xls.ActiveSheetByName, _xls.ActiveFileName);
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