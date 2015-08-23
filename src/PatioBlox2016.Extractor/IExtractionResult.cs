namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using Abstract;

  public interface IExtractionResult
  {
    IEnumerable<IPatchRowExtract> PatchRowExtracts { get; }
    void AddPatchRowExtract(IPatchRowExtract patchRowExtract);
    void AddPatchRowExtractRange(IEnumerable<IPatchRowExtract> patchRowExtracts);

    IEnumerable<string> PatchNames { get; }
    IEnumerable<string> UniqueDescriptions { get; }
    IEnumerable<string> UniqueUpcs { get; }
    IEnumerable<string> UniqueSectionNames { get; }
    IEnumerable<string> UniqueWords { get; }
  }
}