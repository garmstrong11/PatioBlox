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

    public IBarcode Create(IPatchRow row)
    {
      if (row == null) throw new ArgumentNullException(nameof(row));

      if (!row.ItemNumber.HasValue)
        return new NullBarcode(row);

      var length = row.Upc.Length;

      if (string.IsNullOrWhiteSpace(row.Upc))
        return new MissingBarcode(row);

      if (!DigitRegex.IsMatch(row.Upc))
        return new NonNumericBarcode(row);

      if (row.Upc.Length < 12)
        return new TooShortBarcode(row, length);

      if (row.Upc.Length > 13)
        return new TooLongBarcode(row, length);

      var lastDigit = row.Upc.GetLastDigit();

      var digits = row.Upc
        .Take(row.Upc.Length - 1)
        .Reverse()
        .Select(d => Convert.ToInt32(char.GetNumericValue(d)));

      var sum = digits.Select((d, i) => i % 2 == 0 ? d * 3 : d).Sum();

      var checkDigit = (10 - sum % 10) % 10;

      if (lastDigit != checkDigit)
        return new BadCheckDigitBarcode(row, lastDigit, checkDigit);

      return new ValidBarcode(row);
    }
  }
}