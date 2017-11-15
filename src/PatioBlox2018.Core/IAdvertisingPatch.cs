namespace PatioBlox2018.Core
{
  using System.Collections.Generic;

  public interface IAdvertisingPatch
  {
    string Name { get; }
    int StoreCount { get; }

    IReadOnlyCollection<int?> StoreIds { get; }

    void AddStoreId(int? storeId);
    void AddStoreIdRange(IEnumerable<int?> storeIds);
    void RemoveRetailStore(int? storeId);
  }
}