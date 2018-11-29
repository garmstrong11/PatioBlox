namespace PatioBlox2018.Tests.BarcodeTests
{
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2018.Impl.Barcodes;

  [TestFixture]
  public class ValidBarcodeTests
  {
    //[Test]
    //public void Equals_BarcodesWithSameCtorParams_AreEqual()
    //{
    //  const int item = 42;
    //  const string upc = "711680000051";

    //  var bc1 = new ValidBarcode(item, upc);
    //  var bc2 = new ValidBarcode(item, upc);
    //  bc1.Should().Be(bc2);
    //}

    //[Test]
    //public void Equals_BarcodesWithDifferentItemNumbers_AreNotEqual()
    //{
    //  const string upc = "711680000051";

    //  var bc1 = new ValidBarcode(42, upc);
    //  var bc2 = new ValidBarcode(69, upc);

    //  bc1.Should().NotBe(bc2, "barcodes with different item numbers should not be equal");
    //}

    //[Test]
    //public void Equals_BarcodesWithDifferentUpcs_AreNotEqual()
    //{
    //  const int item = 42;

    //  var bc1 = new ValidBarcode(item, "726412175425");
    //  var bc2 = new ValidBarcode(item, "711680000051");

    //  bc1.Should().NotBe(bc2, "barcodes with different UPCs should not be equal");
    //}
  }
}