namespace PatioBlox2018.Impl.Barcodes {
  using System;
  using System.Collections.Generic;
  using PatioBlox2018.Core;

  public abstract class BarcodeBase : IBarcode
  {
    protected IPatchRow PatchRow { get; }
    protected string ErrorFormatString { get; }

    protected int ItemNumber { get; }
    public string Candidate { get; }

    protected BarcodeBase(IPatchRow patchRow)
    {
      PatchRow = patchRow ?? throw new ArgumentNullException(nameof(patchRow));

      ItemNumber = PatchRow.ItemNumber.GetValueOrDefault();
      Candidate = string.IsNullOrEmpty(PatchRow.Upc) ? "Missing" : PatchRow.Upc;
      ErrorFormatString = $"The upc value {Candidate} for item {ItemNumber} {{0}}.";

      Usages = new HashSet<string>(StringComparer.CurrentCultureIgnoreCase);
    }

    public virtual string Value => Candidate;
    public ISet<string> Usages { get; }

    public void AddUsage(string bookName) => Usages.Add(bookName);

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