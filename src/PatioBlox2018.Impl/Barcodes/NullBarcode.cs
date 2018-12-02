namespace PatioBlox2018.Impl.Barcodes
{
  using System.Collections.Generic;
  using PatioBlox2018.Core;

  public class NullBarcode : IBarcode
  {
    public IPatchRow PatchRow { get; }
    public string Value => string.Empty;
    public string Coordinates => $"{PatchRow.PatchName}:{PatchRow.SourceRowIndex}";

    public NullBarcode(IPatchRow patchRow)
    {
      PatchRow = patchRow;
    }

    public string Candidate => string.Empty;
  }
}