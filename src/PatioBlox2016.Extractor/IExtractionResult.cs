﻿namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;

  public interface IExtractionResult
  {
    IEnumerable<PatchDataFile> PatchDataFiles { get; }
    IEnumerable<PatchRowExtract> PatchRowExtracts { get; }
    void AddDataFile(PatchDataFile dataFile);
    void AddPatchRowExtract(PatchRowExtract patchRowExtract);
    void AddPatchRowExtractRange(IEnumerable<PatchRowExtract> patchRowExtracts);

    IEnumerable<string> PatchNames { get; }
    IEnumerable<string> UniqueDescriptions { get; }
    IEnumerable<string> UniqueUpcs { get; }
    IEnumerable<string> UniqueSectionNames { get; }
    IEnumerable<string> UniqueWords { get; }
  }
}