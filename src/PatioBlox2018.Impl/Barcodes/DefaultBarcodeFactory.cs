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

      var digits = candidate
        .Take(candidate.Length - 1)
        .Reverse()
        .Select(d => Convert.ToInt32(char.GetNumericValue(d)));

      var sum = digits.Select((d, i) => i % 2 == 0 ? d * 3 : d).Sum();

      var checkDigit = (10 - sum % 10) % 10;

      if (lastDigit != checkDigit)
        return new BadCheckDigitBarcode(itemNumber, candidate, lastDigit, checkDigit);

      return new ValidBarcode(itemNumber, candidate);
    }
  }
}