namespace PatioBlox.DataExport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
	using System.Threading.Tasks;
	using Domain;
	using FlexCel.Report;

	public class FlexCelReporter : IDisposable
	{
		private readonly FlexCelReport _report;
		public FlexCelReporter()
		{
			_report = new FlexCelReport(true);
		}

		private static string TemplatePath
		{
			get
			{
				var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

				if (assemblyPath == null)
				{
					throw new FileNotFoundException("Unable to find Template path");
				}

				assemblyPath = assemblyPath.Replace(@"file:\", "");

				var appRoot = Path.Combine(assemblyPath, "..", "..", "..");
				var templatePath = Path.Combine(appRoot, @"PatioBlox.DataExport\Templates\Template_1up.xlsx");

				if (!File.Exists(templatePath))
				{
					throw new FileNotFoundException("Unable to find Excel report template");
				}

				return templatePath;
			}
		}

		public List<PatioBlock> Blox { get; set; }

		public string OutputPath { get; set; }

		public async Task RunAsync()
		{
			if (Blox == null)
			{
				throw new InvalidDataException("Blox should not be null");
			}

			if (Blox.Count == 0) return;

			await Task.Run(() =>
			{
				_report.AddTable("Blox", Blox);
				_report.Run(TemplatePath, OutputPath);
			});
		}

		public void Run()
		{
			if (Blox == null)
			{
				throw new InvalidDataException("Blox should not be null");
			}

			if (Blox.Count == 0) return;

			_report.AddTable("Blox", Blox);
			_report.Run(TemplatePath, OutputPath);
		}

		public void Dispose()
		{
			_report.Dispose();
		}
	}
}