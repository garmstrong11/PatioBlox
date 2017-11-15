namespace PatioBlox2018.Core
{
  using System.Collections.Generic;
  using System.Linq;

  public interface IExtractionResult
  {
    IEnumerable<IPatchRowExtract> PatchRowExtracts { get; }
    //void AddPatchRowExtract(IPatchRowExtract patchRowExtract);
    void AddPatchRowExtractRange(IEnumerable<IPatchRowExtract> patchRowExtracts);

    IEnumerable<string> PatchNames { get; }
    //IEnumerable<string> UniqueUpcs { get; }
    //IEnumerable<string> InvalidUpcs { get; }
    //IEnumerable<Barcode> InvalidBarcodes { get; }
    //IEnumerable<Barcode> UniqueBarcodes { get; }

    //IEnumerable<IProduct> GetUniqueProducts();
    //IEnumerable<IPatchRowExtract> GetPatchRowExtracts();
    IEnumerable<IGrouping<string, IPatchRowExtract>> BookGroups { get; }
  }
}