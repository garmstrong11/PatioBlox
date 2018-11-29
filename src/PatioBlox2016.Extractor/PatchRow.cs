namespace PatioBlox2016.Extractor
{
  using PatioBlox2018.Core;

  /// <summary>
  /// A simple data transfer object to encapsulate rows
  /// extracted from an Excel patch content worksheet.
  /// </summary>
  public class PatchRow : IPatchRow
  {
    public PatchRow(
      string patchName, int sourceRowIndex, string section, 
      int? blockIndex, int? sku, string vendor, 
      string description, string palletQuantity, string barcode)
    {
      PatchName = patchName;
      SourceRowIndex = sourceRowIndex;
      Section = section;
      BlockIndex = blockIndex;
      ItemNumber = sku;
      Vendor = vendor;
      Description = description;
      PalletQuantity = palletQuantity;
      Upc = barcode;
    }

    public string PatchName { get; }

    public int SourceRowIndex { get; }

    public string Section { get; }

    public int? BlockIndex { get; }

    public int? ItemNumber { get; }

    public string Vendor { get; }

    public string Description { get; }

    public string PalletQuantity { get; }

    public string Upc { get; }

    public override string ToString()
    {
      return $"{PatchName}: {SourceRowIndex}";
    }

    protected bool Equals(PatchRow other)
    {
      return string.Equals(PatchName, other.PatchName) && SourceRowIndex == other.SourceRowIndex;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((PatchRow) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (PatchName.GetHashCode() * 397) ^ SourceRowIndex;
      }
    }

    public static bool operator ==(PatchRow left, PatchRow right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(PatchRow left, PatchRow right)
    {
      return !Equals(left, right);
    }
  }
}