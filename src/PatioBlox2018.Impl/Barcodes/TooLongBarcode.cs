namespace PatioBlox2018.Impl.Barcodes
{
  public class TooLongBarcode : BarcodeBase
  {
    public TooLongBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate) { }

    public override string Value => 
      string.Format(
        ErrorFormatString, $"is too long ({Length} characters)");
  }
}