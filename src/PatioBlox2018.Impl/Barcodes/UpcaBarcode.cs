namespace PatioBlox2018.Impl.Barcodes
{
  public class UpcaBarcode : BarcodeBase
  {
    public UpcaBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate)
    {
      LastDigit = Candidate.GetLastDigit();
    }

    public override bool IsValid => true;
    public override string Error => string.Empty;
    public override int LastDigit { get; }

    public override int CalculatedCheckDigit =>
      CheckDigitHelpers.CalculateCheckDigit(Candidate, 0);
  }
}