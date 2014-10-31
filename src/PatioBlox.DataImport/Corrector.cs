namespace PatioBlox.DataImport
{
	using Domain;

	public class Corrector : ICorrector
	{
		public static int ItemIdIndex = 5;
		public static int DescriptionIndex = 7;
		public static int PalletIndex = 8;
		public static int UpcIndex = 9;

		public string Filename { get; set; }

		public string PatchName { get; set; }

		public int PatchIndex { get; set; }

		public int RowIndex { get; set; }

		public double ItemId { get; set; }

		public string Description { get; set; }

		public string UPC { get; set; }

		public double PalletQty { get; set; }
	}
}