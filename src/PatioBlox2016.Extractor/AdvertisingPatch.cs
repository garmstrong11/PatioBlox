namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2018.Core;

  public class AdvertisingPatch : IAdvertisingPatch
  {
    private readonly List<int?> _storeIds;

    public AdvertisingPatch(string name)
    {
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

      Name = name;

      _storeIds = new List<int?>();
    }

    public AdvertisingPatch(string name, IEnumerable<int?> stores )
    {
      Name = name;
      _storeIds = new List<int?>(stores);
    }
    
    public string Name { get; }

    public int StoreCount => _storeIds.Count;

    public IReadOnlyCollection<int?> StoreIds => _storeIds.AsReadOnly();

    public void AddStoreId(int? storeId)
    {
      _storeIds.Add(storeId);
    }

    public void AddStoreIdRange(IEnumerable<int?> storeIds)
    {
      _storeIds.AddRange(storeIds);
    }

    public void RemoveRetailStore(int? storeId)
    {
      _storeIds.Remove(storeId);
    }

    public IReadOnlyCollection<int?> Stores => _storeIds.AsReadOnly();


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
      return $"{Name}: {StoreCount} {suffix}";
    }

    #endregion
  }
}