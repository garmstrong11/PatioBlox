namespace PatioBlox2016.Extractor
{
  public class RetailStoreExtract
  {
    public RetailStoreExtract(int region, string patchName, int storeId, string storeName)
    {
      Region = region;
      PatchName = patchName;
      StoreId = storeId;
      StoreName = storeName;
    }
    
    public int Region { get; private set; }
    public string PatchName { get; private set; }
    public int StoreId { get; private set; }
    public string StoreName { get; private set; }

    #region Equality members

    protected bool Equals(RetailStoreExtract other)
    {
      return StoreId == other.StoreId;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((RetailStoreExtract) obj);
    }

    public override int GetHashCode()
    {
      return StoreId;
    }

    #endregion
  }
}