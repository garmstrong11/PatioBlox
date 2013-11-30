namespace PatioBlox.ConsoleDedup
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using DataExport;
	using DataImport;
	using DataImport.Comparers;
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

			var legacyPath = new List<string> {Path.Combine(dataPath, "11_2013 Legacy1up.xlsx")};

			var importer = new PatioBlockImporter(paths);
			var legacyImporter = new LegacyPatioBlockImporter(legacyPath);

			var blokList = importer.PatioBlocks;
			var distinctBlox = blokList.Distinct(new AllPropertiesPatioBlockEqualityComparer())
				.OrderBy(b => b.ItemNumber)
				//.ThenBy(b => b.Barcode)
				.ToList();

			var violators = importer.ItemBarcodeViolations
				.Select(v => new ViolationPatioBlock(v)
					{
					AppearsOn = String.Join(", ", importer.BlockAppearsOnPatches(v))
					})
				.ToList();

			var violationReporter = new FlexCelReporter<ViolationPatioBlock>
				{
				TemplatePath = "Template_Violations.xlsx",
				OutputPath = @"F:\Lowes\Patio Blocks 2014\Mismatches.xlsx",
				Items = violators
			};

			violationReporter.Run();

			violators = importer.BarcodeViolations
				.Select(v => new ViolationPatioBlock(v)
					{
					AppearsOn = String.Join(", ", importer.BlockAppearsOnPatches(v))
					})
				.ToList();

			var barcodeReporter = new FlexCelReporter<ViolationPatioBlock>
				{
				TemplatePath = "Template_Violations.xlsx",
				OutputPath = @"F:\Lowes\Patio Blocks 2014\Bad Barcodes.xlsx",
				Items = violators
				};

			barcodeReporter.Run();

			var oneUps = distinctBlox
				.Select(b => new OneUpPatioBlock(b))
				.OrderBy(b => b.ItemNumber)
				.ToList();

			var legacyBlox = legacyImporter.ImportLegacyPatioBlox();

			var blender = new LegacyDataMerger();
			var blox = blender.MergeData(oneUps, legacyBlox);
			blox = MismatchResolver.ResolveMismatches(blox);

			var oneUpReporter = new FlexCelReporter<OneUpPatioBlock>
				{
				TemplatePath = "Template_1up.xlsx",
				OutputPath = @"F:\Lowes\Patio Blocks 2014\BloxOut.xlsx",
				Items = blox
				};

			oneUpReporter.Run();
		}
	}
}
