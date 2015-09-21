namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using System.IO.Abstractions;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete.Seeding;

  public class DescriptionExtractor : ExtractorBase<DescriptionDto>
  {
    public DescriptionExtractor(IDataSourceAdapter adapter, IFileSystem fileSystem) : base(adapter, fileSystem) {}

    public override IEnumerable<DescriptionDto> Extract()
    {
      var rowCount = XlAdapter.RowCount;
      
      for (var row = 2; row <= rowCount; row++) {
        var desc = new DescriptionDto();

        var name = XlAdapter.ExtractString(row, 5);
        var color = XlAdapter.ExtractString(row, 4);
        var size = XlAdapter.ExtractString(row, 3);
        var vendor = XlAdapter.ExtractString(row, 2);
        var text = XlAdapter.ExtractString(row, 1);

        desc.Name = string.IsNullOrWhiteSpace(name) ? null : name;
        desc.Color = string.IsNullOrWhiteSpace(color) ? null : color;
        desc.Size = string.IsNullOrWhiteSpace(size) ? null : size;
        desc.Vendor = string.IsNullOrWhiteSpace(vendor) ? null : vendor;
        desc.Text = string.IsNullOrWhiteSpace(text) ? null : text;

        yield return desc;
      }
    }

  }
}