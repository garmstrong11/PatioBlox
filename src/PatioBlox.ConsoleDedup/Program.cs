namespace PatioBlox.ConsoleDedup
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
			const string supportPath = @"\\san\AraxiVolume_SAN\Jobs\Lowes_PatioBlocks_1ups\UserDefinedFolders\Support_2013";

			var paths = new List<string>
			{
				Path.Combine(dataPath, "Patio Block_2014 by Patch.xlsx"),
				Path.Combine(dataPath, "Patio Block_2014 by Patch2.xlsx")
			};

			var legacyPath = new List<string> {Path.Combine(dataPath, "11_2013 Legacy1up.xlsx")};

			var importer = new Importer(paths);
			var legacyImporter = new LegacyImporter(legacyPath);


			//Console.WriteLine(importer.SheetCount.ToString());
			var blokList = importer.PatioBlocks;
			var distinctBlox = blokList.Distinct()
				.OrderBy(b => b.ItemNumber)
				.ThenBy(b => b.Barcode)
				.ToList();

			var legacyBlox = legacyImporter.PatioBlocks;

			var blender = new LegacyDataMerger();
			var blox = blender.MergeData(distinctBlox, legacyBlox);

			//var outStr = distinctBlox.Select(b => b.ToString()).ToList();
			//outStr.Insert(0, PatioBlock.HeaderLine);

			//Console.WriteLine("Number of blocks: {0}", blokList.Count);
			//File.WriteAllLines(outPath, outStr);

			var exporter = new FlexCelReporter();
			exporter.OutputPath = @"F:\Lowes\Patio Blocks 2014\BloxOut.xlsx";
			exporter.Blox = blox;
			exporter.Run();
		}
	}
}
