namespace PatioBlox.DataExport
{
	using System.Collections.Generic;
	using System.IO;
	using Domain;

	public class ViolationReporter : FlexCelReporter<ViolationProduct>
	{
		public List<ViolationProduct> Blox { get; set; }

		public override void Run()
		{
			if (Blox == null)
			{
				throw new InvalidDataException("Blox should not be null");
			}

			if (Blox.Count == 0) return;

			Report.AddTable("Blox", Blox);
			Report.Run(TemplatePath, OutputPath);
		} 
	}
}