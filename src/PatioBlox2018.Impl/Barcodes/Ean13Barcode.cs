namespace PatioBlox2018.Impl.Barcodes
{
  public class Ean13Barcode : BarcodeBase
  {
    public Ean13Barcode(int itemNumber, string candidate)
      : base(itemNumber, candidate)
    {
      LastDigit = candidate.GetLastDigit();
    }

    public override bool IsValid => true;
    public override string Error => string.Empty;
    public override int LastDigit { get; }

    public override int CalculatedCheckDigit =>
      CheckDigitHelpers.CalculateCheckDigit(Candidate, 1);
  }
}