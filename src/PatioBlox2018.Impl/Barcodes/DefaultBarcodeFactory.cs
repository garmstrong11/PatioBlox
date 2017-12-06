namespace PatioBlox2018.Impl.Barcodes
{
  using System;
  using System.Linq;
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
      if (string.IsNullOrWhiteSpace(candidate))
        return new MissingBarcode(itemNumber, candidate);

      if (!DigitRegex.IsMatch(candidate))
        return new NonNumericBarcode(itemNumber, candidate);

      if (candidate.Length < 12)
        return new TooShortBarcode(itemNumber, candidate);

      if (candidate.Length > 13)
        return new TooLongBarcode(itemNumber, candidate);

      var lastDigit = candidate.GetLastDigit();
      var checkDigit = CalculateCheckDigit(candidate);

      if (lastDigit != checkDigit)
        return new BadCheckDigitBarcode(itemNumber, candidate, lastDigit, checkDigit);

      return new ValidBarcode(itemNumber, candidate);
    }

    private static int CalculateCheckDigit(string candidate)
    {
      if (string.IsNullOrWhiteSpace(candidate))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(candidate));

      var discriminator = candidate.Length == 13 ? 1 : 0;

      var digits = candidate.GetUpcDigits(discriminator);
      var calculatedCheckDigit = (10 - digits.Sum() % 10) % 10;

      return calculatedCheckDigit;
    }
  }
}