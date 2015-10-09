namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IAdvertisingPatch
  {
    string Name { get; }
    int StoreCount { get; }

    IReadOnlyCollection<int> StoreIds { get; }

    void AddStoreId(int storeId);
    void AddStoreIdRange(IEnumerable<int> storeIds);
    void RemoveRetailStore(int storeId);
  }
}