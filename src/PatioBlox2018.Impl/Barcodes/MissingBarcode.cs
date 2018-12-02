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
      string.Format(ErrorFormatString, "is missing");
  }
}