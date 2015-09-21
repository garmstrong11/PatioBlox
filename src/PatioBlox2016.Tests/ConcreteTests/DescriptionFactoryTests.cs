namespace PatioBlox2016.Tests.ConcreteTests
{
  using System.Collections.Generic;
  using System.Linq;
  using Concrete;
  using ExtractorTests;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;
  using Services.Contracts;
  using Services.EfImpl;

  [TestFixture]
  public class DescriptionFactoryTests : ExtractorTestBase
  {
    private IKeywordRepository _keywordRepo;

    [TestFixtureSetUp]
    public void Init()
    {
      _keywordRepo = A.Fake<IKeywordRepository>();
      A.CallTo(() => _keywordRepo.GetKeywordDictionary()).Returns(BuildKeywordDict());
    }

    [Test]
    public void CanBuildDescription()
    {
      const string subject = "CNTST 16-IN X 12-IN TN/BLACK BLEND DURANGO STONE";
      var factory = new DescriptionFactory(_keywordRepo);
      var description = factory.CreateDescription(subject);

      description.Color.Should().Be("Tan/Black Blend");
      description.Vendor.Should().Be("Countrystone");
      description.Name.Should().Be("Durango Stone");
    }

    private static Dictionary<string, Keyword> BuildKeywordDict()
    {
      var color = new Keyword("COLOR");
      var name = new Keyword("NAME");
      var vendor = new Keyword("VENDOR");
      var country = new Keyword("COUNTRYSTONE") { Parent = vendor };
      var cntst = new Keyword("CNTST") { Parent = country };
      var tan = new Keyword("TAN") { Parent = color };
      var tn = new Keyword("TN") { Parent = tan };
      var black = new Keyword("BLACK") { Parent = color };
      var blend = new Keyword("BLEND") { Parent = color };
      var duran = new Keyword("DURANGO") { Parent = name };
      var stone = new Keyword("STONE") { Parent = name };

      return new List<Keyword>()
             {
               color,
               name,
               vendor,
               country,
               cntst,
               tan,
               tn,
               black,
               blend,
               duran,
               stone
             }.ToDictionary(k => k.Word);
    }
  }
}