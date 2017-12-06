namespace PatioBlox2018.Impl.Barcodes
{
  public class TooShortBarcode : BarcodeBase
  {
    public TooShortBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate) { }

    public override string Value =>
      string.Format(
        ErrorFormatString, $"is too short ({Length} characters)");
  }
}