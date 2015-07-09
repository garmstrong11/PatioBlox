namespace PatioBlox2016.Concrete
{
  public class Cell
  {
		private Cell() {}

	  public Cell(int sourceRowIndex, int sku, int palletQty, string upc) : this()
	  {
		  Id = -1;
		  SourceRowIndex = sourceRowIndex;
		  Sku = sku;
		  PalletQty = palletQty;
		  Upc = upc;
	  }

	  public int Id { get; private set; }
	  public int Index { get; set; }

	  public int SourceRowIndex { get; private set; }

	  public Page Page { get; set; }
	  public int PageId { get; set; }

	  public int Sku { get; private set; }

	  public int DescriptionId { get; set; }
	  public Description Description { get; set; }

	  public int PalletQty { get; set; }
		public string Upc { get; set; }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Sku); }
	  }
  }
}