namespace PatioBlox2016.Tests.ConcreteTests
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Concrete;

  [TestFixture]
  public class DescriptionTests
  {
    private Dictionary<string, Keyword> _keywordDict;
    
    [SetUp]
    public void RunBeforeEachTest()
    {
      var vendorRoot = new Keyword("VENDOR");
      var colorRoot = new Keyword("COLOR");
      var nameRoot = new Keyword("NAME");
      var sizeRoot = new Keyword("SIZE");
      var newRoot = new Keyword("NEW");
      var countryStone = new Keyword("COUNTRYSTONE") {Parent = vendorRoot};
      var cntst = new Keyword("CNTST") {Parent = countryStone};
      var tan = new Keyword("TAN") {Parent = colorRoot};
      var black = new Keyword("BLACK") { Parent = colorRoot };
      var blend = new Keyword("BLEND") { Parent = colorRoot };
      var durango = new Keyword("DURANGO") {Parent = nameRoot};
      var stone = new Keyword("STONE") {Parent = nameRoot};
      var square = new Keyword("SQUARE") {Parent = sizeRoot};
      var sq = new Keyword("SQ") {Parent = square};
      var sand = new Keyword("SAND") {Parent = colorRoot};
      var hill = new Keyword("HILL") {Parent = colorRoot};

      var kList = new List<Keyword>
      {
        vendorRoot, colorRoot, nameRoot, sizeRoot, newRoot,
        countryStone, cntst,
        tan, black, blend,
        durango, stone, square, sq, sand, hill
      };

      _keywordDict = kList.ToDictionary(k => k.Word);
    }
    
    [Test]
    public void Resolve_ReturnsCorrectVendorCompoundString()
    {
      var description = new Description("CNTST 16-IN X 12-IN TAN/BLACK BLEND DURANGO STONE");
      description.Resolve(_keywordDict);

      description.Vendor.Should().Be("Country Stone");
    }

    [Test]
    public void PropertyGet_Size_IsCorrect()
    {
      var description = new Description("CNTST 16-IN X 12-IN TAN/BLACK BLEND DURANGO STONE");
      description.Resolve(_keywordDict);

      description.Size.Should().Be("16-IN x 12-IN");
    }

    [Test]
    public void PropertyGet_SizeWithSq_IsCorrect()
    {
      var description = new Description("CNTST 16-IN SQ TAN/BLACK BLEND DURANGO STONE");
      description.Resolve(_keywordDict);

      description.Size.Should().Be("16-IN Square");
    }

    [Test]
    public void PropertyGet_ColorWithBlend_IsCorrect()
    {
      var description = new Description("CNTST 16-IN SQ TAN/BLACK BLEND DURANGO STONE");
      description.Resolve(_keywordDict);

      description.Color.Should().Be("Tan/Black Blend");
    }

    [Test]
    public void PropertyGet_ColorWithHill_IsCorrect()
    {
      var description = new Description("CNTST 16-IN X 12-IN SAND HILL DURANGO STONE");
      description.Resolve(_keywordDict);

      description.Color.Should().Be("Sand Hill");
    }
  }
}