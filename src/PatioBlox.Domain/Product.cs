namespace PatioBlox.Domain
{
	public class Product
	{
		public int Id { get; set; }
		private string _description;

		public string Location
		{
			get { return string.Format("{0}_{1}", PatchName, Index); }
		}
		public int ItemNumber { get; set; }

		public string Description
		{
			get { return _description; }
			set { _description = value.Trim(); }
		}

		public string PalletQuantity { get; set; }

		public IBarcode Barcode { get; set; }

		public string PatchName { get; set; }
		public int PatchId { get; set; }
		public Section Section { get; set; }
		public int SectionId { get; set; }

		public int Index { get; set; }

		public override string ToString()
		{
			return string.Format("{0}_{1}_{2}_{3}",
				ItemNumber, Barcode, Description, PalletQuantity);
		}

		public override bool Equals(object obj)
		{
			var item = obj as Product;

			if (item == null) return false;

			return ItemNumber == item.ItemNumber
			       && Barcode == item.Barcode;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + ItemNumber;
				hash = hash * 19 + Barcode.GetHashCode();

				return hash;
			}
		}
	}
}