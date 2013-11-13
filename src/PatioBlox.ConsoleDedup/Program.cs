namespace PatioBlox.ConsoleDedup
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using DataImport;
	using Domain;

	internal class Program
	{
		private static void Main(string[] args)
		{
			var paths = new List<string>
			{
				@"F:\Lowes\Patio Blocks 2014\Patio Block_2014 by Patch.xlsx",
				@"F:\Lowes\Patio Blocks 2014\Patio Block_2014 by Patch2.xlsx"
			};
			var importer = new Importer(paths);
			var outPath = @"F:\Lowes\Patio Blocks 2014\BloxOut.txt";

			Console.WriteLine(importer.SheetCount.ToString());
			var blokList = importer.PatioBlocks;
			var distinctBlox = blokList.Distinct()
				.OrderBy(b => b.ItemNumber)
				.ThenBy(b => b.Barcode)
				.ToList();

			var outStr = distinctBlox.Select(b => b.ToString()).ToList();
			outStr.Insert(0, PatioBlock.HeaderLine);

			Console.WriteLine("Number of blocks: {0}", blokList.Count);
			File.WriteAllLines(outPath, outStr);
		}
	}
}
