namespace PatioBlox2016.Tests.ConcreteTests
{
  using Concrete;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class BarcodeTests
  {
    [Test]
    public void EmptyUpc_ReturnsLocationInfo()
    {
      var barcode = new Barcode(string.Empty, "AB", 69);
      barcode.Upc.Should().Be("MissingUpc_PatchAB_Row69");
    }
    
    [TestCase("123456789123", Result = BarcodeType.Upc)]
    [TestCase("1234567891234", Result = BarcodeType.Ean13)]
    [TestCase("123456", Result = BarcodeType.Unknown)]
    public BarcodeType BarcodeTypesAreDerivedFromUpcLength(string upc)
    {
      var barcode = new Barcode(upc, "AB", 6);
      return barcode.BarcodeType;
    }

    [TestCase("123456789123", Result = true)]
    [TestCase("12345Abv89123", Result = false)]
    public bool IsNumeric_FalseIfAlphaUpc(string upc)
    {
      var barcode = new Barcode(upc, "AB", 69);
      return barcode.IsNumeric;
    }

    [TestCase("11111", Result = "-1")]
    [TestCase("12548hh99788", Result = "-1")]
    [TestCase("742786100789", Result = "8")] // Calculation returns correct check digit (8)
    [TestCase("742786308375", Result = "5")]
    public string CalculatedCheckDigit_BadUpc_ReturnsNegOne(string upc)
    {
      var barcode = new Barcode(upc, "AB", 69);
      return barcode.CalculatedCheckDigit;
    }

    [TestCase("123456789123", Result = "3")]
    [TestCase("11111", Result = "1")]
    [TestCase("1234567891234", Result = "4")]
    public string LastDigit_IsCorrect(string upc)
    {
      var barcode = new Barcode(upc, "CT", 17);
      return barcode.LastDigit;
    }
  }
}