namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IAdvertisingPatch
  {
    string Name { get; }
    int RegionId { get; }

    IReadOnlyCollection<IRetailStore> Stores { get; }
    void AddRetailStore(IRetailStore store);
    void AddRetailStoreRange(IEnumerable<IRetailStore> stores);
    void RemoveRetailStore(IRetailStore store);
  }
}