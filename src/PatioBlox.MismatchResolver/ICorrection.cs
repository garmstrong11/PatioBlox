namespace PatioBlox.MismatchResolver
{
	public interface ICorrection
	{
		string Filename { get; set; }
		string SheetName { get; set; }
		int RowIndex { get; set; }
	}
}