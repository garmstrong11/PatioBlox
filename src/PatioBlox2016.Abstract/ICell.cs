namespace PatioBlox2016.Abstract
{
  using Concrete;
  
  public interface ICell
  {
    int Id { get; }
    int SourceRowIndex { get; }
    int Sku { get; }
    int DescriptionId { get; set; }
    IDescription Description { get; set; }
    int PalletQty { get; set; }
    int BarcodeId { get; set; }
    IBarcode Barcode { get; set; }
    ISection Section { get; set; }
    int SectionId { get; set; }
    string Image { get; }
  }
}