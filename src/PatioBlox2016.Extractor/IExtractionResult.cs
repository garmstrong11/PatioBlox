namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using Abstract;
  using PatioBlox2016.Concrete;

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
    IEnumerable<int> UniqueSkus { get; }
    IEnumerable<string> InvalidUpcs { get; }
    IEnumerable<Barcode> InvalidBarcodes { get; } 
  }
}