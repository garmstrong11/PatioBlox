namespace PatioBlox2016.Concrete
{
  public class Cell
  {
		private Cell() {}

	  public Cell(int sourceRowIndex, int sku, int palletQty, string upc) : this()
	  {
		  Id = 0;
		  SourceRowIndex = sourceRowIndex;
		  Sku = sku;
		  PalletQty = palletQty;
	  }

	  public int Id { get; private set; }
	  public int SourceRowIndex { get; private set; }

	  public int Sku { get; private set; }

	  public int DescriptionId { get; set; }
	  public Description Description { get; set; }

	  public int PalletQty { get; set; }

    public int BarcodeId { get; set; }
		public Barcode Barcode { get; set; }

    public Section Section { get; set; }
    public int SectionId { get; set; }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Sku); }
	  }
  }
}