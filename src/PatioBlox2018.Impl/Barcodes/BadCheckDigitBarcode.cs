namespace PatioBlox2018.Impl.Barcodes
{
  public class BadCheckDigitBarcode : BarcodeBase
  {
    public BadCheckDigitBarcode(int itemNumber, string candidate, int lastDigit, int calculatedCheckDigit)
      : base(itemNumber, candidate)
    {
      LastDigit = lastDigit;
      CalculatedCheckDigit = calculatedCheckDigit;
    }

    public override string Error
    {
      get
      {
        var reason = $"has incorrect check digit {LastDigit}. Correct check digit is {CalculatedCheckDigit}";
        return string.Format(ErrorFormatString, reason);
      }
    }

    public override int LastDigit { get; }
    public override int CalculatedCheckDigit { get; }
  }
}