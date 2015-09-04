namespace PatioBlox2016.Tests.ConcreteTests
{
  using System;
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
    //private IRepository<Expansion> _expansionRepo;
    private IRepository<Description> _descriptionRepo; 
    private PatioBloxContext _context;
    private ExtractionResult _extractionResult;

    [TestFixtureSetUp]
    public void Init()
    {
      //var adapter = new FlexCelDataSourceAdapter();
      //var fileSystem = new FileSystem();
      //var indexService = A.Fake<IColumnIndexService>();
      //var extractor = new PatchExtractor(adapter, fileSystem, indexService);
      //extractor.Initialize(Patch1Path);
      ////extractor.Initialize(Patch2Path);

      //A.CallTo(() => indexService.SectionIndex).Returns(2);
      //A.CallTo(() => indexService.ItemIndex).Returns(5);
      //A.CallTo(() => indexService.DescriptionIndex).Returns(7);
      //A.CallTo(() => indexService.PalletQtyIndex).Returns(8);
      //A.CallTo(() => indexService.UpcIndex).Returns(9);

      //_extractionResult = new ExtractionResult();
      //_extractionResult.AddPatchRowExtractRange(extractor.Extract());

      //_context = new PatioBloxContext();

      //_keywordRepo = new RepositoryBase<Keyword>(_context);
      ////_expansionRepo = new RepositoryBase<Expansion>(_context);
      //_descriptionRepo = new RepositoryBase<Description>(_context);
    }

    //[Test]
    //public void CanExtractDescriptions()
    //{
    //  var factory = new DescriptionFactory(_keywordRepo, _expansionRepo);

    //  // Get a list of description Texts that already exist in the db:
    //  var existingDescriptions = _descriptionRepo.GetAll().Select(d => d.Text);

    //  var descriptions = _extractionResult.UniqueDescriptions
    //    .Except(existingDescriptions)
    //    .Select(d => (Description)factory.CreateDescription(d))
    //    .ToList();

    //  var descriptionRepo = new RepositoryBase<Description>(_context);

    //  foreach (var description in descriptions) {
    //    descriptionRepo.Add(description);
    //  }

    //  _context.SaveChanges();

    //  descriptions.Should().NotBeNullOrEmpty();
    //}
  }
}