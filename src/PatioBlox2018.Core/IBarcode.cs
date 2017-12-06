namespace PatioBlox2018.Core
{
  public interface IBarcode
  {
    string Value { get; }
    bool IsValid { get; }
    string Error { get; }
    int LastDigit { get; }
    int CalculatedCheckDigit { get; }
  }
}