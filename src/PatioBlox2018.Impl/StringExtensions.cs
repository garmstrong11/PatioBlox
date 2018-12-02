namespace PatioBlox2018.Impl
{
  using System;
  using System.Linq;
  using System.Threading;

  public static class StringExtensions
  {
    public static int GetLastDigit(this string source)
    {
      var last = source.Last();
      var value = char.GetNumericValue(last);
      var result = Convert.ToInt32(value);

      return result;
    }

    public static string ToTitle(this string source) => 
      Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(source.ToLower());
  }
}