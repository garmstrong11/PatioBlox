namespace PatioBlox2018.Impl.Barcodes
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading;

  public static class CheckDigitHelpers
  {
    public static int CalculateCheckDigit(string candidate, int discriminator)
    {
      var digits = candidate.GetUpcDigits(discriminator);
      var calculatedCheckDigit = (10 - digits.Sum() % 10) % 10;

      return calculatedCheckDigit;
    }
  }
}