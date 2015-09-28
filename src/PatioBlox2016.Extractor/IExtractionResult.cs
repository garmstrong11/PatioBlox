namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;
  using Concrete;

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
    //IEnumerable<string> InvalidUpcs { get; }
    IEnumerable<Barcode> InvalidBarcodes { get; }
    //IEnumerable<Barcode> UniqueBarcodes { get; }

    //IEnumerable<IProduct> GetUniqueProducts();
    IEnumerable<PatchRowExtract> GetProductExtracts();
    IEnumerable<IGrouping<string, IPatchRowExtract>> BookGroups { get; }
  }
}