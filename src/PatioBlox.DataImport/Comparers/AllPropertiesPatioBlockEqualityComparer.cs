namespace PatioBlox.DataImport.Comparers
{
	using System.Collections.Generic;
	using Domain;

	public class AllPropertiesPatioBlockEqualityComparer : IEqualityComparer<Product>
	{
		public bool Equals(Product x, Product y)
		{
			return x.ItemNumber == y.ItemNumber
						 && x.Barcode.Equals(y.Barcode)
						 && x.Description == y.Description
			       && x.PalletQuantity == y.PalletQuantity;
		}

		public int GetHashCode(Product obj)
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + obj.ItemNumber;
				hash = hash * 19 + obj.Barcode.GetHashCode();
				hash = hash * 23 + obj.Description.GetHashCode();
				hash = hash * 19 + obj.PalletQuantity.GetHashCode();

				return hash;
			}
		}
	}
}