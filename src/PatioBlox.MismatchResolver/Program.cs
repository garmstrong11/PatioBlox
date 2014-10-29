using System.Collections.Generic;

namespace PatioBlox.MismatchResolver
{
	using System.IO;

	class Program
	{
		static void Main(string[] args)
		{
			var defaults = Domain.Properties.Settings.Default;

			var dataPath = Path.Combine(
				defaults.FactoryRootPath,
				defaults.SubpathData);

			var patioBloxPaths = new List<string>
			{
				Path.Combine(dataPath, "Patio Block_2015 by Patch.xlsx"),
				Path.Combine(dataPath, "Patio Block_2015 by Patch2 v2.xlsx")
			};

			//var sheetMap
		}
	}
}
