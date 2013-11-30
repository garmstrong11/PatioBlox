﻿namespace PatioBlox.Domain
{
	public interface IBarcode
	{
		BarcodeType BarcodeType { get; }
		bool IsValid { get; }
		string Message { get; }
		string CalculatedCheckDigit { get; }
		string OriginalCheckDigit { get; }
	}
}