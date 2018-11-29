namespace PatioBlox2018.Impl.Barcodes
{
  using PatioBlox2018.Core;

  public class TooLongBarcode : BarcodeBase
  {
    public int Length { get; }

    public TooLongBarcode(IPatchRow patchRow, int length)
      : base(patchRow)
    {
      Length = length;
    }

    public override string Value => 
      string.Format(
        ErrorFormatString, $"is too long ({Length} characters)");
  }
}