namespace PatioBlox2018.Tests.BarcodeTests
{
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2018.Impl.Barcodes;

  [TestFixture]
  public class BarcodeFactoryTests
  {
    [Test]
    public void Creat_ValidUpc_BuildsValidBarcode()
    {
      var fac = new DefaultBarcodeFactory(); 
      const string upc = "7116800002106";

      var bc = fac.Create(382918, upc);
      bc.Value.Should().Be(upc);
    }

    [Test]
    public void Create_IncorrectCheckDigit_BuildsBadCheckDigitBarcode()
    {
      var fac = new DefaultBarcodeFactory();
      const string upc = "7116800002105";

      var bc = fac.Create(382918, upc);
      bc.Should().BeOfType<BadCheckDigitBarcode>();
    }

    [Test]
    public void Create_NoNumbers_BuildsNonNumericBarcode()
    {
      var fac = new DefaultBarcodeFactory();
      const string upc = "#N/A";

      var bc = fac.Create(382918, upc);
      bc.Should().BeOfType<NonNumericBarcode>();
    }
  }
}