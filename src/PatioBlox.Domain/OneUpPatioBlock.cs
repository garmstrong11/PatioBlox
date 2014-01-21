namespace PatioBlox.Domain
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using Properties;

	public class OneUpPatioBlock : PatioBlock
	{
		private static readonly List<AttributeFinder> Finders = AttributeFinder.Finders;
		private string _remnant;
		private readonly string _vendor;
		private readonly string _category;

		private static readonly string SupportPath = Path.Combine(
			Settings.Default.FactoryRootPath, Settings.Default.SubpathSupport);

		private static readonly ILookup<string, string> ImageLookup =
			Directory.GetFiles(SupportPath, "*.psd", SearchOption.AllDirectories)
				.ToLookup(Path.GetFileNameWithoutExtension, Path.GetFileName);

		public OneUpPatioBlock(PatioBlock blok)
		{
			Id = blok.Id;
			ItemNumber = blok.ItemNumber;
			Description = blok.Description;
			PalletQuantity = blok.PalletQuantity;
			Barcode = blok.Barcode;
			PatchName = blok.PatchName;

			_remnant = Description;
			Size = DeriveSize(Description);
			_vendor = Derive(AttributeType.Vendor);
			Color = TitleCase(Derive(AttributeType.Color));
			_category = ProcessCategory(_remnant);

			Name = SetName();
			Image = ImageLookup[ItemNumber.ToString()].FirstOrDefault();
			ApprovalStatus = ApprovalStatus.Pending;
		}

		public OneUpPatioBlock()
		{
		}

		public ApprovalStatus ApprovalStatus { get; set; }

		public string ApprovalStatusString
		{
			get
			{
				return ApprovalStatus.ToString();
			}
		}

		private string SetName()
		{
			var vendorEmpty = string.IsNullOrWhiteSpace(_vendor);
			var categoryEmpty = string.IsNullOrWhiteSpace(_category);

			var joiner = (vendorEmpty || categoryEmpty) ? "" : "|";

			return string.Format("{0}{1}{2}", _vendor, joiner, _category);
		}

		public string Name { get; set; }

		public string Size { get; set; }

		public string Color { get; set; }

		public string Image { get; set; }


		private static string ProcessCategory(string cat)
		{
			if (String.IsNullOrWhiteSpace(cat)) return "";

			var trimmy = Regex.Replace(cat, @"\s\s+", " ").Trim();
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

			return result.Trim();
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
	}
}