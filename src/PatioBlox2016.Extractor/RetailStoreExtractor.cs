namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using System.IO.Abstractions;
  using System.Linq;
  using PatioBlox2016.Abstract;

  public class RetailStoreExtractor : ExtractorBase<RetailStoreExtract>
  {
    private readonly IColumnIndexService _columnIndexService;

    public RetailStoreExtractor(
      IDataSourceAdapter adapter, 
      IFileSystem fileSystem,
      IColumnIndexService columnIndexService) 
      : base(adapter, fileSystem)
    {
      _columnIndexService = columnIndexService;
    }

    #region Overrides of ExtractorBase<IRetailStore>

    public override IEnumerable<RetailStoreExtract> Extract()
    {
      var rowCount = XlAdapter.RowCount;
      var set = new HashSet<RetailStoreExtract>();

      for (var row = 2; row <= rowCount; row++) {
        var patchArea = XlAdapter.ExtractString(row, _columnIndexService.PatchAreaIndex);
        var regionId = XlAdapter.ExtractInteger(row, _columnIndexService.RegionIndex);
        var storeId = XlAdapter.ExtractInteger(row, _columnIndexService.StoreIdIndex);
        var storeName = XlAdapter.ExtractString(row, _columnIndexService.StoreNameIndex);

        // Use a hash set here to eliminate duplicates
        // RetailStoreExtract equality is based on StoreId only
        set.Add(new RetailStoreExtract(regionId, patchArea, storeId, storeName));
      }

      return set.AsEnumerable();
    }

    #endregion
  }
}