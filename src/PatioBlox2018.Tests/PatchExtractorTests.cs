namespace PatioBlox2018.Tests
{
  using System.Collections.Generic;
  using System.Configuration;
  using System.IO;
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

      A.CallTo(() => _indexService.PatchIndex).Returns(1);
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
      var jobRoot = ConfigurationManager.AppSettings["JobRoot"];
      var patchFilePath = ConfigurationManager.AppSettings["BlocksFilename"];
      var path = Path.Combine(jobRoot, patchFilePath);

      var patchRows = _extractor.Extract(path);

      patchRows.Count().Should().Be(11678);
    }
  }
}