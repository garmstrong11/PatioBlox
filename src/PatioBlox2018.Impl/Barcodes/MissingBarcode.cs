﻿namespace PatioBlox2018.Impl.Barcodes
{
  public class MissingBarcode : BarcodeBase
  {
    public MissingBarcode(int itemNumber, string candidate)
      : base(itemNumber, candidate)
    {
      LastDigit = -1;
      CalculatedCheckDigit = -2;
    }

    public override string Error => 
      string.Format(ErrorFormatString, "is missing");

    public override int LastDigit { get; }
    public override int CalculatedCheckDigit { get; }
  }
}