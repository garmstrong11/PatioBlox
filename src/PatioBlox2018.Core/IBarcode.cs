namespace PatioBlox2018.Core
{
  public interface IBarcode
  {
    string Value { get; }
    bool IsValid { get; }
  }
}