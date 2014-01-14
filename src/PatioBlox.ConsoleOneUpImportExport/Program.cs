namespace PatioBlox.ConsoleOneUpImportExport
{
  using DataExport;
  using DataImport;
  using Domain;
  using Domain.Properties;
  using System;
  using System.Collections.Generic;
  using System.IO;

  internal class Program
	{
		private static void Main(string[] args)
		{
			var def = Settings.Default;

			var dataPath = Path.Combine(def.FactoryRootPath, def.SubPathReport);

			var paths = new List<string>
				{
				Path.Combine(dataPath, "BloxOut_Edited.xlsx"),
				};

			var importer = new OneUpImporter(paths);
		  var blox = importer.Blocks;
		  var exporter = new OneUpJsonExporter(blox);

		  //var jsonStr = exporter.BloxArray;

			string jsonDict;
			if (!exporter.TryGetJsonDict(out jsonDict))
			{
				Console.WriteLine("Product data contains errors!");
				Console.ReadLine();
				return;
			};

		  var outStr = String.Format("var products = {0}", jsonDict);
		  var outPath = Path.Combine(def.FactoryRootPath, def.SubpathData, def.ProductDataFileName);
		  File.WriteAllText(outPath, outStr);

			outPath = Path.Combine(def.FactoryRootPath, def.SubpathJsx, def.JobFoldersFileName);
			File.WriteAllText(outPath, JobFolders.GetJsxString());

		  Console.WriteLine("Success!");
		  Console.ReadLine();
		}
	}
}
