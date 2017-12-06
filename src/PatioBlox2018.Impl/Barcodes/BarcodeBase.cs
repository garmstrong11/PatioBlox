namespace PatioBlox2018.Impl.Barcodes {
  using PatioBlox2018.Core;

  public abstract class BarcodeBase : IBarcode
  {
    protected string ErrorFormatString { get; }

    protected int ItemNumber { get; }
    protected string Candidate { get; }

    protected BarcodeBase(int itemNumber, string candidate)
    {
      if (!string.IsNullOrWhiteSpace(candidate))
      {
        ItemNumber = itemNumber;
        Candidate = candidate;
        Length = Candidate.Length;
        ErrorFormatString = $"The upc value '{Candidate}' for item '{ItemNumber}' {{0}}.";
      }
      else {
        Candidate = string.Empty;
        Length = 0;
      }
    }

    public virtual string Value => Candidate;
    public int Length { get; }
  }
}