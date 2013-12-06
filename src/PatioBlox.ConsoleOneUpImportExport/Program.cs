namespace PatioBlox.ConsoleOneUpImportExport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using DataImport;
	using Domain;
	using Domain.Properties;
	using Newtonsoft.Json;

	internal class Program
	{
		private static void Main(string[] args)
		{
			var defaults = Settings.Default;

			var dataPath = Path.Combine(
				defaults.FactoryRootPath,
				defaults.SubPathReport);

			var paths = new List<string>
				{
				Path.Combine(dataPath, "BloxOut.xlsx"),
				};

			var importer = new OneUpImporter(paths);

			var blokDict = importer.BlockDict;

			var json = JsonConvert.SerializeObject(blokDict, Formatting.Indented);

			Console.WriteLine(json);
		}
	}
}
