namespace PatioBlox2018.Tests
{
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Extractor;

  [TestFixture]
  public class PatchRowExtractTests
  {
    [Test]
    public void Extract_ShowsIntellisense()
    {
      var extract = new PatchRowExtract("AB", 17, "", 55, 1254, "", "Rasta", "17", "74581254785");
      extract.Description.Should().Be("Rasta");
    }
  }
}