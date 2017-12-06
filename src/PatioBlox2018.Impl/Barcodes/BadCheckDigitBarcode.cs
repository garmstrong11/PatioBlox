namespace PatioBlox2018.Impl.Barcodes
{
  public class BadCheckDigitBarcode : BarcodeBase
  {
    private int LastDigit { get; }
    private int CheckDigit { get; }

    public BadCheckDigitBarcode(int itemNumber, string candidate, int lastDigit, int checkDigit)
      : base(itemNumber, candidate)
    {
      LastDigit = lastDigit;
      CheckDigit = checkDigit;
    }

    public override string Value
    {
      get
      {
        var reason = $"has incorrect check digit {LastDigit}. Correct check digit is {CheckDigit}";
        return string.Format(ErrorFormatString, reason);
      }
    }
  }
}