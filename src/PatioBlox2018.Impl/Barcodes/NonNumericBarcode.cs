namespace PatioBlox2018.Impl.Barcodes
{
  using PatioBlox2018.Core;

  public class NonNumericBarcode : BarcodeBase
  {
    public NonNumericBarcode(IPatchRow patchRow)
      : base(patchRow) { }

    public override string Value 
      => string.Format(ErrorFormatString, "is not numeric");
  }
}