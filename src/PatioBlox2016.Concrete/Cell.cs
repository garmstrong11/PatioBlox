namespace PatioBlox2016.Concrete
{
  public class Cell
  {
		private Cell() {}

	  public Cell(int index)
	  {
		  Id = -1;
		  Index = index;
	  }

	  public int Id { get; private set; }
	  public int Index { get; set; }

	  public Page Page { get; set; }
	  public int PageId { get; set; }

		public Product Product { get; set; }
	  public int ProductId { get; set; }

	  public int PalletQty { get; set; }
		public string Upc { get; set; }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Product.Sku); }
	  }
  }
}