namespace PatioBlox2018.Core
{
  public interface IBarcodeFactory
  {
    IBarcode Create(int itemNumber, string candidate);
  }
}