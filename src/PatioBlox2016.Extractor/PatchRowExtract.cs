namespace PatioBlox2016.Extractor
{
  using Abstract;

  public class PatchRowExtract : IPatchRowExtract
  {
    public PatchRowExtract(string patchName, int rowIndex)
    {
      PatchName = patchName;
      RowIndex = rowIndex;
    }

    public string PatchName { get; private set; }
    public int RowIndex { get; private set; }

    public int Sku { get; set; }
    public string Description { get; set; }
    public string Section { get; set; }
    public string PalletQuanity { get; set; }
    public string Upc { get; set; }

    protected bool Equals(PatchRowExtract other)
    {
      return Sku == other.Sku 
        && string.Equals(PatchName, other.PatchName) 
        && RowIndex == other.RowIndex 
        && string.Equals(Description, other.Description) 
        && string.Equals(Section, other.Section) 
        && string.Equals(PalletQuanity, other.PalletQuanity) 
        && string.Equals(Upc, other.Upc);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((PatchRowExtract) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        var hashCode = Sku;
        hashCode = (hashCode * 397) ^ PatchName.GetHashCode();
        hashCode = (hashCode * 397) ^ RowIndex;
        hashCode = (hashCode * 397) ^ Description.GetHashCode();
        hashCode = (hashCode * 397) ^ Section.GetHashCode();
        hashCode = (hashCode * 397) ^ PalletQuanity.GetHashCode();
        hashCode = (hashCode * 397) ^ Upc.GetHashCode();
        return hashCode;
      }
    }

    public override string ToString()
    {
      return string.Format("RowIndex: {0}, Sku: {1}, Description: {2}", RowIndex, Sku, Description);
    }
  }
}