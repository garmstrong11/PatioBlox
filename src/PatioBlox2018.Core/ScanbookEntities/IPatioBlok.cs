namespace PatioBlox2018.Core.ScanbookEntities {
  public interface IPatioBlok
  {
    IPage Page { get; }
    int ItemNumber { get; }
    string Vendor { get; }
    string Description { get; }
    string PalletQuantity { get; }
    string Barcode { get; }
    int SourceRowIndex { get; }
  }
}