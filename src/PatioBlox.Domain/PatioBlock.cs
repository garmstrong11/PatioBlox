namespace PatioBlox.Domain
{
	public class PatioBlock
	{
		public string Id { get; set; }
		public int ItemNumber { get; set; }
		public string Description { get; set; }
		public string PalletQuantity { get; set; }
		public string Barcode { get; set; }
		public string Patch { get; set; }

		public string Name { get; set; }
		public string Size { get; set; }
		public string Color { get; set; }
		public int Sequence { get; set; }
		public string Image { get; set; }

		public override string ToString()
		{
			return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t",
				ItemNumber, Barcode, Description, PalletQuantity, Id);
		}

		public override bool Equals(object obj)
		{
			var item = obj as PatioBlock;

			if (item == null) return false;

			return ItemNumber == item.ItemNumber
			       && Description == item.Description
			       && PalletQuantity == item.PalletQuantity
			       && Barcode == item.Barcode;
		}

		public override int GetHashCode()
		{
			return (ItemNumber + Barcode).GetHashCode();
		}

		public static string HeaderLine
		{
			get
			{
				return string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t",
				"ItemNumber", "Barcode", "Description", "PalletQuantity", "Id"); 
			}
		}
	}
}