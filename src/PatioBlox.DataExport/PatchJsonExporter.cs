namespace PatioBlox.DataExport
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Domain;
	using Domain.Properties;
	using MoreLinq;
	using Newtonsoft.Json.Linq;

	public class PatchJsonExporter
	{
		private readonly List<Patch> _patches;

		public PatchJsonExporter(List<Patch> patches)
		{
			_patches = patches;
		}

		public string PatchDict
		{
			get
			{
				var jsonResult = new JObject(
					_patches.Select(p => new JProperty(p.Name,
						new JObject(
							new JProperty("name", p.Name),
							new JProperty("pages", GetPagesArray(p.Sections)))))
					);

				return String.Format("var patches = {0}", jsonResult.ToString());
			}
		}

		public string OutputPath { get; set; }

		private JArray GetPagesArray(List<Section> sections)
		{
			var resultArray = new JArray();

			foreach (var section in sections) {
				var buckets = section.PatioBlocks.Batch(4);

				foreach (var bucket in buckets) {
					var page = new JObject(
						new JProperty("header", section.Name),
						new JProperty("blocks",
							new JArray(bucket.Select(b => new JValue(
								String.Format("{0}_{1}.pdf", b.ItemNumber, b.Barcode.Value))))));

					resultArray.Add(page);
				}
			}

			return resultArray;
		}

		public void ExportToJsonFile()
		{
			if (String.IsNullOrWhiteSpace(OutputPath)) {
				throw new InvalidOperationException("Output path is not specified");
			}

			var dir = Path.GetDirectoryName(OutputPath);
			if (dir == null || !Directory.Exists(dir)) {
				throw new InvalidOperationException("Output directory does not exist");
			}

			File.WriteAllText(OutputPath, PatchDict);
		}
	}
}