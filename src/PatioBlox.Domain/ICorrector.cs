namespace PatioBlox.Domain
{
	public interface ICorrector
	{
		string Filename { get; set; }
		string PatchName { get; set; }
		int PatchIndex { get; set; }
		int RowIndex { get; set; }
		double ItemId { get; set; }
		string Description { get; set; }
		string UPC { get; set; }
		double PalletQty { get; set; }
	}
}