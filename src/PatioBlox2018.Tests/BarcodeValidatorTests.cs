namespace PatioBlox2018.Tests
{
  using NUnit.Framework;
  using PatioBlox2018.Impl.Barcodes;

  [TestFixture]
  public class BarcodeValidatorTests
  {
    [TestCase("742786309495", 12, 13, ExpectedResult = true, Reason = "Valid UpcA upc accepted")]
    [TestCase("5034504935778", 12, 13, ExpectedResult = true, Reason = "Valid EAN-13 upc accepted")]
    [TestCase("", 12, 13, ExpectedResult = false, Reason = "Empty candidate rejected")]
    [TestCase(null, 12, 13, ExpectedResult = false, Reason = "Null candidate rejected")]
    [TestCase("  ", 12, 13, ExpectedResult = false, Reason = "Whitespace only candidate rejected")]
    [TestCase("ASDF", 12, 13, ExpectedResult = false, Reason = "Non-numeric candidate rejected")]
    [TestCase("2345", 12, 13, ExpectedResult = false, Reason = "Short candidate rejected")]
    [TestCase("2345234523452345", 12, 13, ExpectedResult = false, Reason = "Long candidate rejected")]
    [TestCase("742786309490", 12, 13, ExpectedResult = false, Reason = "Bad UpcA check digit rejected")]
    [TestCase("5034504935770", 12, 13, ExpectedResult = false, Reason = "Bad EAN-13 check digit rejected")]
    public bool BarcodeValidationCompositeTest(string candidate, int minChars, int maxChars)
    {
      return BarcodeValidator.ValidateBarcode(candidate, minChars, maxChars).IsSuccess;
    }
  }
}