namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;

  public interface IExtractionResult
  {
    IEnumerable<PatchDataFile> PatchDataFiles { get; }
    IEnumerable<PatchRowExtract> PatchRowExtracts { get; }
    void AddDataFile(PatchDataFile dataFile);
    void AddPatchRowExtract(PatchRowExtract patchRowExtract);
  }
}