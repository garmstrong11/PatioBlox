namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class BlockAttributes
	{
		private string _vendor;
		private string _category;
		private string _color;

		public string Vendor
		{
			get { return _vendor; }
			set { _vendor = value.Trim(); }
		}

		public string Category	
		{
			get { return _category; }
			set
			{
				var trimmy = Regex.Replace(value, @"\s\s+", "").Trim();
				var title = TitleCase(trimmy);

				var dict = new Dictionary<string, string>
					{
					{"Countrymanor", "Country Manor"},
					{"Cm", "Country Manor"},
					{"Geometrc", "Geometric"},
					{"Stne", "Stone"},
					{"Edgr", "Edger"}
					};

				foreach (var key in dict.Keys) {
					if (title.Contains(key)) {
						title = title.Replace(key, dict[key]);
					}
				}

				_category = title;
			}
		}

		public string Size { get; set; }

		public string Color	
		{
			get { return _color; }
			set { _color = TitleCase(value.Trim()); }
		}

		public string Name	
		{
			get
			{
				var vendorEmpty = string.IsNullOrWhiteSpace(Vendor);
				var categoryEmpty = string.IsNullOrWhiteSpace(Category);

				var joiner = (vendorEmpty || categoryEmpty) ? "" : "|";

				return string.Format("{0}{1}{2}", Vendor, joiner, Category);
			}
		}

		private static string TitleCase(string s)
		{
			return string.IsNullOrWhiteSpace(s) ? "" : new string(CharsToTitleCase(s).ToArray());
		}

		private static IEnumerable<char> CharsToTitleCase(string s)
		{
			var wordSeparators = new List<char> { ' ', '/', '\'' };
			var newWord = true;
			foreach (var c in s)
			{
				if (newWord) { yield return Char.ToUpper(c); newWord = false; }
				else yield return Char.ToLower(c);
				if (wordSeparators.Contains(c)) newWord = true;
			}
		}
	}
}