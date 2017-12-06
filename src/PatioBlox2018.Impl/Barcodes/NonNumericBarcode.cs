namespace PatioBlox2018.Impl.Barcodes
{
  public class NonNumericBarcode : BarcodeBase
  {
    public NonNumericBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate) { }

    public override string Value 
      => string.Format(ErrorFormatString, "is not numeric");
  }
}