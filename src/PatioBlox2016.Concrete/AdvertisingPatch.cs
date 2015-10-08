namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2016.Abstract;

  public class AdvertisingPatch : IAdvertisingPatch
  {
    private readonly List<IRetailStore> _stores;

    public AdvertisingPatch(string name, int regionId)
    {
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

      Name = name;
      RegionId = regionId;

      _stores = new List<IRetailStore>();
    }
    
    public string Name { get; private set; }
    public int RegionId { get; private set; }

    public IReadOnlyCollection<IRetailStore> Stores
    {
      get { return _stores.AsReadOnly(); }
    }

    public void AddRetailStore(IRetailStore store)
    {
      store.AdertisingPatch = this;
      _stores.Add(store);
    }

    public void AddRetailStoreRange(IEnumerable<IRetailStore> stores)
    {
      var storesToAdd = stores.ToList();

      foreach (var store in storesToAdd) {
        store.AdertisingPatch = this;
      }

      _stores.AddRange(storesToAdd);
    }

    public void RemoveRetailStore(IRetailStore store)
    {
      store.AdertisingPatch = null;
      _stores.Remove(store);
    }

    #region Equality members

    protected bool Equals(AdvertisingPatch other)
    {
      return Stores.SequenceEqual(other.Stores)
        && string.Equals(Name, other.Name) 
        && RegionId == other.RegionId;
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
        hashCode = (hashCode*397) ^ RegionId;
        return hashCode;
      }
    }

    #endregion
  }
}