namespace PatioBlox.Domain
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class OneUpPatioBlock : PatioBlock
	{
		private static readonly List<AttributeFinder> Finders = AttributeFinder.Finders;
		private string _remnant;
		private readonly string _vendor;
		private readonly string _size;
		private readonly string _category;
		private readonly string _color;
		//private const string SupportPath = @"\\san\AraxiVolume_SAN\Jobs\Lowes_PatioBlocks_1ups\UserDefinedFolders\Support_2013";

		//private static readonly ILookup<string, string> ImageLookup = 
		//	Directory.GetFiles(SupportPath, "*.*", SearchOption.AllDirectories)
		//		.ToLookup(Path.GetFileNameWithoutExtension, Path.GetFileName);

		public OneUpPatioBlock(PatioBlock blok)
		{
			Id = blok.Id;
			ItemNumber = blok.ItemNumber;
			Description = blok.Description;
			PalletQuantity = blok.PalletQuantity;
			Barcode = blok.Barcode;
			Patch = blok.Patch;

			_remnant = Description.Replace("BLEND", "");
			_size = DeriveSize(Description);
			_vendor = Derive(AttributeType.Vendor);
			_category = ProcessCategory(Derive(AttributeType.Category));
			_color = _remnant.Trim();
		}

		public string Name
		{
			get
			{
				var vendorEmpty = string.IsNullOrWhiteSpace(_vendor);
				var categoryEmpty = string.IsNullOrWhiteSpace(_category);

				var joiner = (vendorEmpty || categoryEmpty) ? "" : "|";

				return string.Format("{0}{1}{2}", _vendor, joiner, _category);
			}
		}

		public string Size
		{
			get { return _size; }
		}

		public string Color
		{
			get { return TitleCase(_color.Trim()); }
		}

		//public string Image
		//{
		//	get
		//	{
		//		return ImageLookup[ItemNumber.ToString()].FirstOrDefault();
		//	}
		//}

		public string Image { get; set; }

		private static string ProcessCategory(string cat)
		{
			if (String.IsNullOrWhiteSpace(cat)) return "";

			var trimmy = Regex.Replace(cat, @"\s\s+", "").Trim();
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

			return title;
		}

		private string Derive(AttributeType attributeType)
		{
			var result = string.Empty;
			var finders = Finders.Where(f => f.Type == attributeType).ToList();

			foreach (var finder in finders)
			{
				var match = Regex.Match(_remnant, finder.SearchExpression);
				if (!match.Success) continue;

				var foundVal = match.Groups[0].Value;
				result = string.IsNullOrEmpty(finder.Expansion) ? foundVal : finder.Expansion;
				_remnant = _remnant.Replace(foundVal, "");
			}

			return result;
		}

		private string DeriveSize(string desc)
		{
			var regex = new Regex(@"(\d+\.?\d*)-?(IN|SQ ?FT)? ?X?");
			var matches = regex.Matches(desc);

			if (matches.Count < 1) return String.Empty;

			var match = matches[0];
			var result = match.Groups[1].Value;

			result += DeriveUnit(match.Groups[2].Value);

			_remnant = _remnant.Replace(match.Groups[0].Value, "");

			if (matches.Count > 1)
			{
				match = matches[1];
				result += string.Format(" x {0}", match.Groups[1].Value);
				result += DeriveUnit(match.Groups[2].Value);
				_remnant = _remnant.Replace(match.Groups[0].Value, "");
			}

			if (Regex.IsMatch(desc, " H "))
			{
				result += " H";
				_remnant = _remnant.Replace(" H ", "");
			}

			if (Regex.IsMatch(desc, "SQUARE"))
			{
				result += " Square";
				_remnant = _remnant.Replace("SQUARE", "");
			}

			return result;
		}

		private static string DeriveUnit(string str)
		{
			var unitFinder = Finders
				.Where(f => f.Type == AttributeType.Unit)
				.ToList();

			var unit = unitFinder
				.FirstOrDefault(u => Regex.IsMatch(str, u.SearchExpression));

			return unit != null ? unit.Expansion : "";
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

		//private static ILookup<string, string> ImageDict
		//{
		//	get
		//	{
		//		const string supportPath = @"\\san\AraxiVolume_SAN\Jobs\Lowes_PatioBlocks_1ups\UserDefinedFolders\Support_2013";

		//		return Directory.GetFiles(supportPath, "*.*", SearchOption.AllDirectories)
		//			.ToLookup(Path.GetFileNameWithoutExtension, Path.GetFileName);
		//	}
		//}
	}
}