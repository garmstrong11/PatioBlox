namespace PatioBlox2018.Impl.Barcodes
{
  public class TooLongBarcode : BarcodeBase
  {
    public TooLongBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate)
    {
      LastDigit = -1;
      CalculatedCheckDigit = -2;
    }

    public override string Error => 
      string.Format(
        ErrorFormatString, $"is too long ({Candidate.Length} characters)");

    public override int LastDigit { get; }
    public override int CalculatedCheckDigit { get; }
  }
}