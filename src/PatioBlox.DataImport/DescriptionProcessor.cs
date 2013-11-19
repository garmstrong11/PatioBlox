namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	public class DescriptionProcessor
	{
		private static List<AttributeFinder> _finders = AttributeFinder.Finders;

		public static BlockAttributes Process(string desc)
		{
			string remnant;
			desc = desc.Replace("BLEND", "");
			var attr = new BlockAttributes();

			attr.Size = DeriveSize(desc, out remnant);
			attr.Vendor = Derive(remnant, AttributeType.Vendor, out remnant);
			attr.Category = Derive(remnant, AttributeType.Category, out remnant);
			attr.Color = remnant.Trim();

			return attr;
		}

		private static string Derive(string desc, AttributeType attributeType, out string remnant)
		{
			remnant = desc;
			var result = string.Empty;
			var finders = _finders.Where(f => f.Type == attributeType).ToList();

			foreach (var finder in finders)
			{
				var match = Regex.Match(desc, finder.SearchExpression);
				if (!match.Success) continue;

				var foundVal = match.Groups[0].Value;
				result = string.IsNullOrEmpty(finder.Expansion) ? foundVal : finder.Expansion;
				remnant = remnant.Replace(foundVal, "");
			}

			return result;
		}

		private static string DeriveSize(string desc, out string remnant)
		{
			var regex = new Regex(@"(\d+\.?\d*)-?(IN|SQ ?FT)? ?X?");
			var matches = regex.Matches(desc);
			remnant = desc;

			if (matches.Count < 1) return String.Empty;

			var match = matches[0];
			var result = match.Groups[1].Value;
			
			result += DeriveUnit(match.Groups[2].Value);

			remnant = remnant.Replace(match.Groups[0].Value, "");

			if (matches.Count > 1)
			{
				match = matches[1];
				result += string.Format(" x {0}", match.Groups[1].Value);
				result += DeriveUnit(match.Groups[2].Value);
				remnant = remnant.Replace(match.Groups[0].Value, "");
			}

			if (Regex.IsMatch(desc, " H "))
			{
				result += " H";
				remnant = remnant.Replace(" H ", "");
			}

			if (Regex.IsMatch(desc, "SQUARE"))
			{
				result += " Square";
				remnant = remnant.Replace("SQUARE", "");
			}

			return result;
		}

		private static string DeriveUnit(string str)
		{
			var unitFinder = _finders
				.Where(f => f.Type == AttributeType.Unit)
				.ToList();

			var unit = unitFinder
				.FirstOrDefault(u => Regex.IsMatch(str, u.SearchExpression));

			return unit != null ? unit.Expansion : "";
		}
	}
}