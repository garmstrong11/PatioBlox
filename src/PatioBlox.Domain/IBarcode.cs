namespace PatioBlox.Domain
{
	public interface IBarcode
	{
		BarcodeType BarcodeType { get; } 
		string Message { get; }
		string CalculatedCheckDigit { get; }
		string OriginalCheckDigit { get; }
	}
}