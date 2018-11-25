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
        ErrorFormatString = $"The upc value {Candidate} for item {ItemNumber} {{0}}.";
      }
      else {
        Candidate = string.Empty;
        Length = 0;
      }
    }

    public virtual string Value => Candidate;
    public int Length { get; }

    protected bool Equals(BarcodeBase other)
    {
      return ItemNumber == other.ItemNumber 
             && string.Equals(Candidate, other.Candidate);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((BarcodeBase) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (ItemNumber * 397) ^ Candidate.GetHashCode();
      }
    }

    public static bool operator ==(BarcodeBase left, BarcodeBase right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(BarcodeBase left, BarcodeBase right)
    {
      return !Equals(left, right);
    }
  }
}