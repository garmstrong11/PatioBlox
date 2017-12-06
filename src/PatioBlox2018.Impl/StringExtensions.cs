namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public static class StringExtensions
  {
    //public static (IEnumerable<int> digits, int lastDigit) GetUpcDigits(this string source)
    //{
    //  var len = source.Length;

    //  var digits = source
    //    .Select(i => Convert.ToInt32(char.GetNumericValue(i)))
    //    .ToList();

    //  return (digits.Take(len - 1), digits.Last());
    //}

    public static int GetLastDigit(this string source) => 
      Convert.ToInt32(source.Last());

    public static IEnumerable<int> GetUpcDigits(this string source, int discriminator) =>
      source
        .Select(d => Convert.ToInt32(char.GetNumericValue(d)))
        .Take(source.Length - 1)
        .Select(s => s % 2 == discriminator ? s : s)
        .ToList();
  }
}