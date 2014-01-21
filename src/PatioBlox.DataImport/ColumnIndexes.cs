namespace PatioBlox.DataImport
{
	using System.Collections.Generic;

	public class ColumnIndexes
	{
		public int Section { get; internal set; }
		public int Item { get; internal set; }
		public int Description { get; internal set; }
		public int PalletQty { get; internal set; }
		public int Barcode { get; internal set; }

		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}, {3}, {4}",
				Section, Item, Description, PalletQty, Barcode);
		}

		public Dictionary<string, int> GetValueDict()
		{
			return new Dictionary<string, int>
				{
				{"Section", Section},
				{"Item", Item},
				{"Description", Description},
				{"PalletQty", PalletQty},
				{"Barcode", Barcode}
				};
		}
	}
}