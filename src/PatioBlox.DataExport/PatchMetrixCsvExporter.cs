namespace PatioBlox.DataExport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Domain;

	public class PatchMetrixCsvExporter
	{
		private readonly List<Patch> _patches;
		public PatchMetrixCsvExporter(List<Patch> patches)
		{
			_patches = patches;
		}

		public string OutputPath { get; set; }

		public void ExportToCsv()
		{
			var csvString = _patches.Select(p => p.ToMetrixString());

			File.WriteAllText(OutputPath, String.Join("", csvString));
		}
	}
}