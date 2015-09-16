namespace PatioBlox2016.Concrete
{
  public class Cell
  {
	  public int SourceRowIndex { get; set; }

	  public int Sku { get; set; }

    //public string Vendor { get; set; }

    public string Color { get; set; }

    public string Size { get; set; }

    public string Name { get; set; }

	  public string PalletQty { get; set; }

		public string Upc { get; set; }

    public Section Section { get; set; }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Sku); }
	  }
  }
}