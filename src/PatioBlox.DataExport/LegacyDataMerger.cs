namespace PatioBlox.DataExport
{
	using System.Collections.Generic;
	using System.Linq;
	using Domain;

	public class LegacyDataMerger
	{
		public List<OneUpPatioBlock> MergeData(List<OneUpPatioBlock> currentBlox, List<LegacyPatioBlock> legacyBlox)
		{
			foreach (var blok in currentBlox) {
				var legacyBlok = legacyBlox
					.FirstOrDefault(b => b.ItemNumber == blok.ItemNumber);
					//.FirstOrDefault(b => b.ItemNumber == blok.ItemNumber && b.Barcode == blok.Barcode);

				if (legacyBlok == null) continue;

				//blok.Name = legacyBlok.Name;
				//blok.Size = legacyBlok.Size;
				//blok.Color = legacyBlok.Color;
				//blok.Image = legacyBlok.Image;
			}
			
			return currentBlox;
		}
	}
}