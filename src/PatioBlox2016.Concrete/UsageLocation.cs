namespace PatioBlox2016.Concrete
{
  using Abstract;

  public class UsageLocation : IUsageLocation
  {
    public UsageLocation(string patchName, int rowIndex)
    {
      PatchName = patchName;
      RowIndex = rowIndex;
    }
    
    public string PatchName { get; private set; }
    public int RowIndex { get; private set; }

    protected bool Equals(UsageLocation other)
    {
      return string.Equals(PatchName, other.PatchName) && RowIndex == other.RowIndex;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((UsageLocation) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (PatchName.GetHashCode()*397) ^ RowIndex;
      }
    }

    public override string ToString()
    {
      return string.Format("{0}_{1}", PatchName, RowIndex);
    }
  }
}