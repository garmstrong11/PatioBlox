namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.IO.Abstractions;
  using System.Linq;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;

  public class AdvertisingPatchExtractor : ExtractorBase<IAdvertisingPatch>, IAdvertisingPatchExtractor
  {
    private readonly IColumnIndexService _columnIndexService;

    public AdvertisingPatchExtractor(
      IDataSourceAdapter adapter, 
      IFileSystem fileSystem,
      IColumnIndexService columnIndexService) 
      : base(adapter, fileSystem)
    {
      _columnIndexService = columnIndexService;
    }

    public override IEnumerable<IAdvertisingPatch> Extract()
    {
      var rowCount = XlAdapter.RowCount;
      var result = new List<Tuple<string, int>>();

      for (var row = 2; row <= rowCount; row++) {
        var patchArea = XlAdapter.ExtractString(row, _columnIndexService.PatchAreaIndex);
        var storeId = XlAdapter.ExtractInteger(row, _columnIndexService.StoreIdIndex);
        
        // Disallow duplicate storeIds:
        if (result.Any(p => p.Item2 == storeId)) continue;

        result.Add(new Tuple<string, int>(patchArea, storeId));
      }

      var patchGroups = result.GroupBy(t => t.Item1);
      return patchGroups
        .Select(patchGroup => new AdvertisingPatch(patchGroup.Key, patchGroup.Select(p => p.Item2)));
    }
  }
}