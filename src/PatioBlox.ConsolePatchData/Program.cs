namespace PatioBlox.ConsolePatchData
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using DataExport;
	using DataImport;
	using Domain;

	internal class Program
	{
		private static void Main(string[] args)
		{
			const string dataPath = @"F:\Lowes\Patio Blocks 2014\factory\data\orig";

			var paths = new List<string>
				{
				Path.Combine(dataPath, "Patio Block_2014 by Patch.xlsx"),
				Path.Combine(dataPath, "Patio Block_2014 by Patch2.xlsx")
				};

			var storeListPath = new List<string>
				{
				Path.Combine(dataPath, "storeListBasic.xls")
				};

			var stores = new StoreListImporter(storeListPath);

			var importer = new PatchPatioBlockImporter(paths, stores);

			var patches = importer.ImportPatches();

			//var matcher = new PatchMatcher(patches);
			//var matchList = matcher.MatchedPatches;

			var reporter = new FlexCelReporter<Patch>
				{
				TemplatePath = "Template_PatchReport.xlsx",
				OutputPath = @"F:\Lowes\Patio Blocks 2014\PageCount.xlsx",
				Items = patches
				};
			reporter.Run();

			var patchDict = patches.ToDictionary(p => p.Name);

			Console.WriteLine("Job contains {0} patches", patches.Count);
		}
	}
}
