namespace PatioBlox2016.Tests.ExtractorTests
{
  using System.IO;
  using System.IO.Abstractions;
  using System.Linq;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Extractor;

  [TestFixture]
  public class AdvertisingPatchExtractorTests : ExtractorTestBase
  {
    private IColumnIndexService _indexService;

    [TestFixtureSetUp]
    public void Init()
    {
      _indexService = A.Fake<IColumnIndexService>();
      A.CallTo(() => _indexService.PatchAreaIndex).Returns(5);
      A.CallTo(() => _indexService.StoreIdIndex).Returns(6);
    }
    
    [Test]
    public void CanExtract()
    {
      var dapter = new FlexCelDataSourceAdapter();
      var fileSystem = new FileSystem();

      var extractor = new AdvertisingPatchExtractor(dapter, fileSystem, _indexService);
      extractor.Initialize(StoreListPath);

      var patches = extractor.Extract();

      patches.Count().Should().Be(216);
    }

    [Test]
    public void DuplicatePatchIds_AreRejected()
    {
      var dapter = A.Fake<IDataSourceAdapter>();
      A.CallTo(() => dapter.RowCount).Returns(3);
      A.CallTo(() => dapter.ColumnCount).Returns(8);

      // dapter returns identcal ints from 2 different rows:
      A.CallTo(() => dapter.ExtractInteger(2, 6)).Returns(69);
      A.CallTo(() => dapter.ExtractInteger(3, 6)).Returns(69);
      A.CallTo(() => dapter.ExtractString(2, A<int>._)).Returns("GA");

      var fileSystem = A.Fake<IFileSystem>();
      A.CallTo(() => fileSystem.File.Exists(A<string>._)).Returns(true);

      var extractor = new AdvertisingPatchExtractor(dapter, fileSystem, _indexService);
      extractor.Initialize("kabong!");

      var result = extractor.Extract();

      result.Count().Should().Be(1);
    }
  }
}