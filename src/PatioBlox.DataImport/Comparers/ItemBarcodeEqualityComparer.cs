namespace PatioBlox.DataImport.Comparers
{
	using System.Collections.Generic;
	using Domain;

	public class ItemBarcodeEqualityComparer : IEqualityComparer<PatioBlock>
	{
		public bool Equals(PatioBlock x, PatioBlock y)
		{
			return x.ItemNumber == y.ItemNumber 
				&& x.Barcode == y.Barcode;
		}

		public int GetHashCode(PatioBlock obj)
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + obj.ItemNumber;
				hash = hash * 19 + obj.Barcode.GetHashCode();

				return hash;
			}
		}
	}
}