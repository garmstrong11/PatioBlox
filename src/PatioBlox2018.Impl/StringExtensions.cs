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
    public static Option<IEnumerable<int>> GetBarcodeDigits(
      this string source, Option<IEnumerable<int>> factors)
    {
      if (factors.IsNone) return None;

      var sourceNums = source.Select(GetIntForNumeral).Take(source.Length - 1);
      return factors.Map(f => f.Zip(sourceNums, (fac, src) => fac * src));

      //return factors.Bind(source.Select(GetIntForNumeral).Take(source.Length - 10).Zip(factors, (d, f) => d * f)
      //);
      //return source
      //  .Select(GetIntForNumeral)
      //  .Take(source.Length - 1)
      //  .Zip(factors, (digit, factor) => digit * factor)
      //  .ToList();
    }

    private static int GetIntForNumeral(char numeral) 
      => Convert.ToInt32(char.GetNumericValue(numeral));

    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> sequence, int count)
    {
      var seq = sequence.ToList();
      while (count-- > 0)
      {
        foreach (var item in seq)
          yield return item;
      }
    }
  }
}