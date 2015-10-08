namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using PatioBlox2016.Abstract;

  public interface IStoreExtractionResult
  {
    IEnumerable<IAdvertisingPatch> AdvertisingPatches { get; }
    IEnumerable<IRetailStore> RetailStores { get; } 
  }
}