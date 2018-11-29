namespace PatioBlox2018.Core
{
  public interface IBarcodeFactory
  {
    IBarcode Create(IPatchRow patchRow);
  }
}