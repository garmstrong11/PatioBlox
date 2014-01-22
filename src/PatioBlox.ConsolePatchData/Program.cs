namespace PatioBlox.ConsolePatchData
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using DataExport;
	using DataImport;
	using Domain;

	internal class Program
	{
		private static void Main()
		{
			var defaults = Domain.Properties.Settings.Default;

			var dataPath = Path.Combine(
				defaults.FactoryRootPath,
				defaults.SubpathData);

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

			//var importer = new PatchPatioBlockImporter(paths, stores);

			//var patches = importer.ImportPatches();

			//var reporter = new FlexCelReporter<Patch>
			//	{
			//	TemplatePath = "Template_PatchReport.xlsx",
			//	OutputPath = Path.Combine(defaults.FactoryRootPath, defaults.SubPathReport, "PageCount.xlsx"),
			//	Items = patches
			//	};
			//reporter.Run();

			//var exporter = new PatchJsonExporter(patches)
			//	{
			//	OutputPath = Path.Combine(defaults.FactoryRootPath, defaults.SubpathData, "PatchData.jsx")
			//	};
			//exporter.ExportToJsonFile();

			//var metrix = new PatchMetrixCsvExporter(patches)
			//	{
			//	OutputPath = Path.Combine(defaults.FactoryRootPath, defaults.SubPathReport, "PatchPages.csv")
			//	};
			//metrix.ExportToCsv();

			//Console.WriteLine("Job contains {0} patches", patches.Count);
		}
	}
}
