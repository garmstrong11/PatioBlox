namespace PatioBlox2018.Impl.Barcodes {
  using PatioBlox2018.Core;

  public abstract class BarcodeBase : IBarcode
  {
    protected string ErrorFormatString { get; }

    protected int ItemNumber { get; }
    protected string Candidate { get; }

    protected BarcodeBase(int itemNumber, string candidate)
    {
      if (string.IsNullOrWhiteSpace(candidate)) Candidate = string.Empty;

      ItemNumber = itemNumber;
      Candidate = candidate;
      ErrorFormatString = $"The upc value '{Candidate}' for item '{ItemNumber}' {{0}}.";
    }

    public virtual string Value => Candidate;
    public virtual bool IsValid => false;
    public abstract string Error { get; }
    public abstract int LastDigit { get; }

    public abstract int CalculatedCheckDigit { get; }
  }
}