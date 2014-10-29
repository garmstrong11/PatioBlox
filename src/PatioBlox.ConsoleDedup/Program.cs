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
			var defaults = Domain.Properties.Settings.Default;

			var dataPath =  Path.Combine(
				defaults.FactoryRootPath,
				defaults.SubpathData);

			var paths = new List<string>
			{
				Path.Combine(dataPath, "Patio Block_2015 by Patch.xlsx"),
				Path.Combine(dataPath, "Patio Block_2015 by Patch2 v2.xlsx")
			};

			var importer = new PatioBlockImporter(paths);

			var blokList = importer.PatioBlocks;
			var distinctBlox = blokList.Distinct(new AllPropertiesPatioBlockEqualityComparer())
				.OrderBy(b => b.ItemNumber)
				.ThenBy(b => b.Barcode.ToString())
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
				OutputPath = Path.Combine(
					defaults.FactoryRootPath, 
					defaults.SubPathReport, 
					"Mismatches.xlsx"),
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
				OutputPath = Path.Combine(
					defaults.FactoryRootPath,
					defaults.SubPathReport, 
					"Bad Barcodes.xlsx"),
				Items = violators
				};

			barcodeReporter.Run();

			var oneUps = distinctBlox
				.Select(b => new OneUpPatioBlock(b))
				.OrderBy(b => b.ItemNumber)
				.ToList();

			//var blox = MismatchResolver.ResolveMismatches(oneUps);

			var oneUpReporter = new FlexCelReporter<OneUpPatioBlock>
				{
				TemplatePath = "Template_1up.xlsx",
				OutputPath = Path.Combine(
					defaults.FactoryRootPath,
					defaults.SubPathReport, 
					"BloxOut.xlsx"),
				Items = blox
				};

			oneUpReporter.Run();
		}
	}
}
