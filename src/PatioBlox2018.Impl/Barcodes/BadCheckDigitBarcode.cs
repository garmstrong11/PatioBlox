namespace PatioBlox2018.Impl.Barcodes
{
  using PatioBlox2018.Core;

  public class BadCheckDigitBarcode : BarcodeBase
  {
    private int LastDigit { get; }
    private int CheckDigit { get; }

    public BadCheckDigitBarcode(IPatchRow patchRow, int lastDigit, int checkDigit)
      : base(patchRow)
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