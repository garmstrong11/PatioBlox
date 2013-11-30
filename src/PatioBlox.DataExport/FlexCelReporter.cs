namespace PatioBlox.DataExport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Reflection;
	using System.Threading.Tasks;
	using FlexCel.Report;

	public class FlexCelReporter<T> : IDisposable
	{
		protected readonly FlexCelReport Report;
		private string _templateName;

		public FlexCelReporter()
		{
			Report = new FlexCelReport(true);
		}

		public string TemplatePath
		{
			get { return _templateName; }
			set
			{
				var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

				if (assemblyPath == null)
				{
					throw new FileNotFoundException("Unable to find Template path");
				}

				assemblyPath = assemblyPath.Replace(@"file:\", "");

				var appRoot = Path.Combine(assemblyPath, "..", "..", "..");
				var templatePath = Path.Combine(appRoot, @"PatioBlox.DataExport\Templates", value);

				if (!File.Exists(templatePath))
				{
					throw new FileNotFoundException("Unable to find Excel report template");
				}

				_templateName = templatePath;
			}
		}

		public List<T> Items { get; set; }

		public string OutputPath { get; set; }

		public virtual async Task RunAsync()
		{
			if (Items == null)
			{
				throw new InvalidDataException("Blox should not be null");
			}

			if (Items.Count == 0) return;

			await Task.Run(() =>
			{
				Report.AddTable("Blox", Items);
				Report.Run(TemplatePath, OutputPath);
			});
		}

		public virtual void Run()
		{
			{
				if (Items == null)
				{
					throw new InvalidDataException("Blox should not be null");
				}

				if (Items.Count == 0) return;

				Report.AddTable("Items", Items);
				Report.Run(TemplatePath, OutputPath);
			}
		}

		public void Dispose()
		{
			Report.Dispose();
		}
	}
}