namespace PatioBlox.ConsolePatchData
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using DataImport;

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

			var importer = new PatchPatioBlockImporter(paths);

			var patches = importer.ImportPatches();

			var patchDict = patches.ToDictionary(p => p.Name);

			Console.WriteLine("Job contains {0} patches", patches.Count);
		}
	}
}
