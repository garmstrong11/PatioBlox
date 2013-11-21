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
			var item = obj as PatioBlock;

			if (item == null) return false;

			return ItemNumber == item.ItemNumber
			       //&& Description == item.Description
			       //&& PalletQuantity == item.PalletQuantity
			       && Barcode == item.Barcode;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + ItemNumber;
				hash = hash * 19 + Barcode.GetHashCode();
				//hash = hash * 23 + Description.GetHashCode();
				//hash = hash * 19 + PalletQuantity.GetHashCode();

				return hash;
			}
		}
	}
}