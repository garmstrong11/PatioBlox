﻿namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class CellDuplicateComparer : IEqualityComparer<Cell>
	{
		public bool Equals(Cell x, Cell y)
		{
			return x.Sku == y.Sku
				&& x.DescriptionId == y.DescriptionId
				&& x.PalletQty == y.PalletQty
				&& x.BarcodeId == y.BarcodeId;
		}

		public int GetHashCode(Cell cell)
		{
			unchecked
			{
				var hashCode = cell.Sku;
				hashCode = (hashCode * 397) ^ cell.DescriptionId;
				hashCode = (hashCode * 397) ^ cell.PalletQty;
				hashCode = (hashCode * 397) ^ cell.BarcodeId;
				return hashCode;
			}
		}
	}
}