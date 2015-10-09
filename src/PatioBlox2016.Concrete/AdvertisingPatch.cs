namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2016.Abstract;

  public class AdvertisingPatch : IAdvertisingPatch
  {
    private readonly List<int> _storeIds;

    public AdvertisingPatch(string name)
    {
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

      Name = name;

      _storeIds = new List<int>();
    }

    public AdvertisingPatch(string name, IEnumerable<int> stores )
    {
      Name = name;
      _storeIds = new List<int>(stores);
    }
    
    public string Name { get; private set; }

    public int StoreCount
    {
      get { return _storeIds.Count; }
    }

    public IReadOnlyCollection<int> StoreIds
    {
      get { return _storeIds.AsReadOnly(); }
    }

    public void AddStoreId(int storeId)
    {
      _storeIds.Add(storeId);
    }

    public void AddStoreIdRange(IEnumerable<int> storeIds)
    {
      _storeIds.AddRange(storeIds);
    }

    public void RemoveRetailStore(int storeId)
    {
      _storeIds.Remove(storeId);
    }

    public IReadOnlyCollection<int> Stores
    {
      get { return _storeIds.AsReadOnly(); }
    }


    #region Equality members

    protected bool Equals(AdvertisingPatch other)
    {
      return Stores.SequenceEqual(other.Stores)
        && string.Equals(Name, other.Name);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((AdvertisingPatch) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        var hashCode = Stores.GetHashCode();
        hashCode = (hashCode*397) ^ Name.GetHashCode();
        return hashCode;
      }
    }

    #endregion

    #region Overrides of Object

    public override string ToString()
    {
      var suffix = StoreCount == 1 ? "store" : "stores";
      return string.Format("{0}: {1} {2}", Name, StoreCount, suffix);
    }

    #endregion
  }
}