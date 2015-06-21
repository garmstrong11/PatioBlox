namespace PatioBlox2016.Concrete
{
  using System.Text.RegularExpressions;

  public static class Extensions
	{
		public static string FlipSlashes(this string source)
		{
			return Regex.Replace(source, @"\\", "/");
		}

		public static string AddExtension(this string source, string extension)
		{
			return string.Format("{0}.{1}", source, extension);
		}
	}
}