namespace PatioBlox.ConsoleOneUpImportExport
{
  using DataExport;
  using DataImport;
  using Domain.Properties;
  using System;
  using System.Collections.Generic;
  using System.IO;

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
		  var blox = importer.Blocks;
		  var exporter = new OneUpJsonExporter(blox);

			//var blokDict = importer.BlockDict;
		  var jsonStr = exporter.BloxArray;

		  string jsonDict;
		  if (!exporter.TryGetJsonDict(out jsonDict))
		  {
		    Console.WriteLine("Product data contains errors!");
		    Console.ReadLine();
        return;
		  };

		  var outStr = String.Format("var products = {0}", jsonDict);
		  var outPath = Path.Combine(defaults.FactoryRootPath, defaults.SubPathReport, "productData.jsx");
		  File.WriteAllText(outPath, outStr);

		  Console.WriteLine("Success!");
		  Console.ReadLine();

		  //var json = JsonConvert.SerializeObject(blokDict, 
		  //  Formatting.Indented, 
		  //  new JsonSerializerSettings { ContractResolver =  new OneUpPatioBlockContractResolver()});

		  //Console.WriteLine(jsonStr);
		}
	}
}
