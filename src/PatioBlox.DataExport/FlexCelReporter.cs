namespace PatioBlox.DataExport
{
	using System;
	using System.IO;
	using System.Reflection;
	using FlexCel.Report;

	public class FlexCelReporter : IDisposable
	{
		protected readonly FlexCelReport Report;
		private static string _templateName;

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

		//public List<OneUpPatioBlock> Blox { get; set; }

		public string OutputPath { get; set; }

		//public virtual async Task RunAsync()
		//{
		//	if (Blox == null)
		//	{
		//		throw new InvalidDataException("Blox should not be null");
		//	}

		//	if (Blox.Count == 0) return;

		//	await Task.Run(() =>
		//	{
		//		Report.AddTable("Blox", Blox);
		//		Report.Run(TemplatePath, OutputPath);
		//	});
		//}

		public virtual void Run()
		{
		}

		public void Dispose()
		{
			Report.Dispose();
		}
	}
}