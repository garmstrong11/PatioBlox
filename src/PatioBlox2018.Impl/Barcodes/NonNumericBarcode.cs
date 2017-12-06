namespace PatioBlox2018.Impl.Barcodes
{
  public class NonNumericBarcode : BarcodeBase
  {
    public NonNumericBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate) { }

    public override string Error 
      => string.Format(ErrorFormatString, "is not numeric");

    public override int LastDigit => -1;
    public override int CalculatedCheckDigit => -1;
  }
}