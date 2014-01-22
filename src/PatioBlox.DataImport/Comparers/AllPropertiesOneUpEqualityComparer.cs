namespace PatioBlox.DataImport.Comparers
{
	using System.Collections.Generic;
	using Domain;

	public class AllPropertiesOneUpEqualityComparer : IEqualityComparer<OneUpProduct>
	{
		public bool Equals(OneUpProduct x, OneUpProduct y)
		{
			return x.ItemNumber == y.ItemNumber
			       && x.Barcode.Equals(y.Barcode)
			       && x.Description == y.Description
			       && x.PalletQuantity == y.PalletQuantity;
		}

		public int GetHashCode(OneUpProduct obj)
		{
			unchecked {
				int hash = 17;
				hash = hash * 23 + obj.ItemNumber;
				hash = hash * 19 + obj.Barcode.GetHashCode();
				hash = hash * 23 + obj.Description.GetHashCode();
				hash = hash * 19 + obj.PalletQuantity.GetHashCode();

				return hash;
			}
		}
	}
}