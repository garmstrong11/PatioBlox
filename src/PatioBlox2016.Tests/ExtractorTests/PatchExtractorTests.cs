namespace PatioBlox2016.Tests.ExtractorTests
{
  using System.IO.Abstractions;
  using System.Linq;
  using Abstract;
  using Extractor;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class PatchExtractorTests : ExtractorTestBase
  {
    private IColumnIndexService _indexService;
    private FlexCelDataSourceAdapter _adapter;
    private IFileSystem _fileSystem;
    private IPatchExtractor _extractor;

    [TestFixtureSetUp]
    public void Init()
    {
      _adapter = new FlexCelDataSourceAdapter();
      _fileSystem = new FileSystem();
      _indexService = A.Fake<IColumnIndexService>();
      _extractor = new PatchExtractor(_adapter, _fileSystem, _indexService);
      _extractor.Initialize(Patch1Path);

      A.CallTo(() => _indexService.SectionIndex).Returns(2);
      A.CallTo(() => _indexService.ItemIndex).Returns(5);
      A.CallTo(() => _indexService.DescriptionIndex).Returns(7);
      A.CallTo(() => _indexService.PalletQtyIndex).Returns(8);
      A.CallTo(() => _indexService.UpcIndex).Returns(9);
    }
    
    [SetUp]
    public void RunBeforeEachTest()
    {
      
    }
    
    [Test]
    public void ExtractPatchNames_PatchCountIsCorrect()
    {
      var actual = _extractor.ExtractPatchNames().ToList();

      actual.Count().Should().Be(106);
      actual.First().Should().Be("AB");
      actual.Last().Should().Be("NP");
    }

    [Test]
    public void CanExtractAllPatches()
    {
      var patchRows = _extractor.Extract();

      patchRows.Count().Should().Be(4978);
    }

	  [Test]
	  public void ExtractOnePatch_NoItemNumber_ExtractsWithItemNumberAsNegative1()
	  {
		  var patchRows = _extractor.ExtractOnePatch("ED");

		  patchRows.Any(p => p.Sku == -1).Should().BeTrue();
	  }
  }
}