namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public static class StringExtensions
  {
    public static int GetLastDigit(this string source) 
      => GetIntForNumeral(source.Last());

    public static IEnumerable<int> GetUpcDigits(this string source, int discriminator) =>
      source
        .Select(GetIntForNumeral)
        .Take(source.Length - 1)
        .Select((s,i) => i % 2 == discriminator ? s * 3 : s)
        .ToList();

    private static int GetIntForNumeral(char numeral) 
      => Convert.ToInt32(char.GetNumericValue(numeral));
  }
}