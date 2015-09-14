namespace PatioBlox2016.Concrete
{
  public class Cell
  {
	  public int SourceRowIndex { get; set; }

	  public int Sku { get; set; }

	  public Description Description { get; set; }

	  public string PalletQty { get; set; }

		public string Upc { get; set; }

    public Section Section { get; set; }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Sku); }
	  }
  }
}