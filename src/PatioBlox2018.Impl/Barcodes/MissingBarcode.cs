namespace PatioBlox2018.Impl.Barcodes
{
  public class MissingBarcode : BarcodeBase
  {
    public MissingBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate) { }

    public override string Value => 
      string.Format(ErrorFormatString, "is missing");
  }
}