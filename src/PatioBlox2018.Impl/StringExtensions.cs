namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using LanguageExt;
  using static LanguageExt.Prelude;

  public static class StringExtensions
  {
    public static Option<int> GetLastDigit(this string source)
    {
      if (string.IsNullOrWhiteSpace(source)) return None;

      var last = source.ToCharArray().Last();
      var digit = Convert.ToInt32(char.GetNumericValue(last));

      return digit == -1 ? None : Some(digit);
    } 

    /// <summary>
    /// Gets a sequence of digits derived from the characters in source,
    /// multiplied by the elements of a generated sequence of factors.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="factors"></param>
    /// <returns></returns>
    public static IEnumerable<int> GetBarcodeDigits(this string source, IEnumerable<int> factors) =>
      source
        .Select(GetIntForNumeral)
        .Take(source.Length - 1)
        .Zip(factors, (digit, factor) => digit * factor)
        .ToList();

    private static int GetIntForNumeral(char numeral) 
      => Convert.ToInt32(char.GetNumericValue(numeral));
  }
}