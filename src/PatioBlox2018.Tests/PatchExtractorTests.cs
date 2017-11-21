namespace PatioBlox2018.Tests
{
  using System.Collections.Generic;
  using System.Linq;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Extractor;
  using PatioBlox2018.Core;

  [TestFixture]
  public class PatchExtractorTests : ExtractorTestBase
  {
    private IColumnIndexService _indexService;
    private FlexCelDataSourceAdapter _adapter;
    private IExtractor<IPatchRow> _extractor;

    [SetUp]
    public void Init()
    {
      _adapter = new FlexCelDataSourceAdapter();
      _indexService = A.Fake<IColumnIndexService>();
      _extractor = new PatchExtractor(_adapter, _indexService);

      A.CallTo(() => _indexService.SectionIndex).Returns(2);
      A.CallTo(() => _indexService.PageOrderIndex).Returns(3);
      A.CallTo(() => _indexService.ItemNumberIndex).Returns(5);
      A.CallTo(() => _indexService.VendorIndex).Returns(6);
      A.CallTo(() => _indexService.DescriptionIndex).Returns(7);
      A.CallTo(() => _indexService.PalletQtyIndex).Returns(8);
      A.CallTo(() => _indexService.BarcodeIndex).Returns(9);
    }

    [Test]
    public void CanExtractAllPatches()
    {
      var files = new List<string> {Patch3Path};
      var patchRows = _extractor.Extract(files);

      patchRows.Count().Should().Be(13876);
    }
  }
}