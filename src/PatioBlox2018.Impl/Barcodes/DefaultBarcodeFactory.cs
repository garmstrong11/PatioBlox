namespace PatioBlox2018.Impl.Barcodes
{
  using System.Text.RegularExpressions;
  using PatioBlox2018.Core;

  public class DefaultBarcodeFactory : IBarcodeFactory
  {
    private static Regex DigitRegex { get; }

    static DefaultBarcodeFactory()
    {
      DigitRegex = new Regex(@"^\d+$", RegexOptions.Compiled);
    }

    public IBarcode Create(int itemNumber, string candidate)
    {
      IBarcode result;

      if (string.IsNullOrWhiteSpace(candidate))
        return new MissingBarcode(itemNumber, candidate);

      if (!DigitRegex.IsMatch(candidate))
        return new NonNumericBarcode(itemNumber, candidate);

      if (candidate.Length < 12)
        return new TooShortBarcode(itemNumber, candidate);

      if (candidate.Length > 13)
        return new TooLongBarcode(itemNumber, candidate);

      if (candidate.Length == 12)
        result = new UpcaBarcode(itemNumber, candidate);
      else result = new Ean13Barcode(itemNumber, candidate);

      if (result.CalculatedCheckDigit != result.LastDigit)
        result = new BadCheckDigitBarcode(
          itemNumber, candidate, result.LastDigit, result.CalculatedCheckDigit);

      return result;
    }
  }
}