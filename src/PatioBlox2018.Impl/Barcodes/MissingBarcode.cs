namespace PatioBlox2018.Impl.Barcodes
{
  using PatioBlox2018.Core;

  public class MissingBarcode : BarcodeBase
  {
    public MissingBarcode(IPatchRow patchRow)
      : base(patchRow)
    {
    }

    public override string Value => 
      $"Missing Barcode for item {ItemNumber} on row {PatchRow.SourceRowIndex} in patch {PatchRow.PatchName}";
  }
}