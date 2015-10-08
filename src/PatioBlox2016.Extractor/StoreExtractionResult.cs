namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;

  public class StoreExtractionResult : IStoreExtractionResult
  {
    private readonly List<RetailStoreExtract> _extracts;

    public StoreExtractionResult()
    {
      _extracts = new List<RetailStoreExtract>();
    }

    public void AddRetailStoreExtracts(IEnumerable<RetailStoreExtract> extracts)
    {
      _extracts.AddRange(extracts);
    }

    #region Implementation of IStoreExtractionResult

    public IEnumerable<IAdvertisingPatch> AdvertisingPatches
    {
      get
      {
        var patchGroups = _extracts.GroupBy(e => e.PatchName);

        foreach (var groop in patchGroups) {
          var first = groop.First();
          var patch = new AdvertisingPatch(groop.Key, first.Region);
          patch.AddRetailStoreRange(groop.Select(r => new RetailStore(r.StoreName, r.StoreId)));

          yield return patch;
        }
      }
    }

    public IEnumerable<IRetailStore> RetailStores
    {
      get { return AdvertisingPatches.SelectMany(p => p.Stores).OrderBy(s => s.Id); }
    }

    #endregion
  }
}