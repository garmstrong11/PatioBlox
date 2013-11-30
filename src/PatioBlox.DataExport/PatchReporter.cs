namespace PatioBlox.DataExport
{
	using System.Collections.Generic;
	using System.IO;
	using Domain;

	public class PatchReporter : FlexCelReporter<Patch>
	{
		public List<Patch> Patches { get; set; }

		public override void Run()
		{
			if (Patches == null)
			{
				throw new InvalidDataException("Patches collection should not be null");
			}

			if (Patches.Count == 0) return;

			Report.AddTable("Patches", Patches);
			Report.Run(TemplatePath, OutputPath);
		} 
	}
}