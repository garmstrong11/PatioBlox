namespace PatioBlox2018.Impl.Barcodes
{
  using PatioBlox2018.Core;

  public class TooShortBarcode : BarcodeBase
  {
    public int Length { get; }

    public TooShortBarcode(IPatchRow patchRow, int length)
      : base(patchRow)
    {
      Length = length;
    }

    public override string Value =>
      string.Format(
        ErrorFormatString, $"is too short ({Length} characters)");
  }
}