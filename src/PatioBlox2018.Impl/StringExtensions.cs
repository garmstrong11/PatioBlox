namespace PatioBlox2018.Impl
{
  using System;
  using System.Linq;

  public static class StringExtensions
  {
    public static int GetLastDigit(this string source)
    {
      var last = source.Last();
      var value = char.GetNumericValue(last);
      var result = Convert.ToInt32(value);

      return result;
    }
  }
}