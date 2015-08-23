namespace PatioBlox2016.Tests.ConcreteTests
{
  using System.IO.Abstractions;
  using System.Linq;
  using Abstract;
  using Concrete;
  using DataAccess;
  using Extractor;
  using ExtractorTests;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;
  using Services.EfImpl;

  [TestFixture]
  public class DescriptionFactoryTests : ExtractorTestBase
  {
    private IRepository<Keyword> _keywordRepo;
    private IRepository<Expansion> _expansionRepo;
    private PatioBloxContext _context;
    private ExtractionResult _extractionResult;

    [TestFixtureSetUp]
    public void Init()
    {
      var adapter = new FlexCelDataSourceAdapter();
      var fileSystem = new FileSystem();
      var indexService = A.Fake<IColumnIndexService>();
      var extractor = new PatchExtractor(adapter, fileSystem, indexService);
      extractor.Initialize(Patch1Path);

      A.CallTo(() => indexService.SectionIndex).Returns(2);
      A.CallTo(() => indexService.ItemIndex).Returns(5);
      A.CallTo(() => indexService.DescriptionIndex).Returns(7);
      A.CallTo(() => indexService.PalletQtyIndex).Returns(8);
      A.CallTo(() => indexService.UpcIndex).Returns(9);

      _extractionResult = new ExtractionResult();
      _extractionResult.AddPatchRowExtractRange(extractor.Extract());

      _context = new PatioBloxContext();

      _keywordRepo = new RepositoryBase<Keyword>(_context);
      _expansionRepo = new RepositoryBase<Expansion>(_context);
    }

    [Test]
    public void CanExtractDescriptions()
    {
      var factory = new DescriptionFactory(_keywordRepo, _expansionRepo);
      var descriptions = _extractionResult.UniqueDescriptions
        .Select( d => factory.CreateDescription(d));

      descriptions.Should().NotBeNullOrEmpty();
    }
  }
}