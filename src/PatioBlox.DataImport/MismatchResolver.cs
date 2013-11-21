namespace PatioBlox.DataImport
{
	using System.Collections.Generic;
	using System.Linq;
	using Comparers;
	using Domain;

	public class MismatchResolver
	{
		public static List<OneUpPatioBlock> ResolveMismatches(List<OneUpPatioBlock> blox)
		{
			var bloxToRemove = new List<OneUpPatioBlock>();

			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 54329 && b.PalletQuantity == "84"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 460716 && b.PalletQuantity == "56"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 80097 && b.Description == "FULTON 4INX11.5IN WALNUT WALL BLK"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548616 && b.Description == "16X6 COUNTRY MANOR RETAINING WALL"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548924 && b.Description == "A+R LUXORA 3-IN Color TBD"));
			bloxToRemove.AddRange(blox.Where(b => b.ItemNumber == 548951 && b.Description == "A+R LUXORA 3-IN Color TBD"));
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