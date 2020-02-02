namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using PatioBlox2018.Core;

  public class PatchExtractor : ExtractorBase<IPatchRow>
  {
    private IColumnIndexService IndexService { get; }

    public PatchExtractor(IDataSourceAdapter adapter, IColumnIndexService indexService)
      : base(adapter)
    {
      IndexService = indexService;
    }

    public override IEnumerable<IPatchRow> Extract(string excelFilePath)
    {
      if (string.IsNullOrWhiteSpace(excelFilePath))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(excelFilePath));

      Initialize(excelFilePath);

      var rowCount = XlAdapter.RowCount;
      const int startRowIndex = 2;

      for (var row = startRowIndex; row <= rowCount; row++) {
        var patchName = XlAdapter.ExtractString(row, IndexService.PatchIndex);
        var sec = XlAdapter.ExtractString(row, IndexService.SectionIndex);
        var addr = XlAdapter.ExtractInteger(row, IndexService.PageOrderIndex);
        var desc = XlAdapter.ExtractString(row, IndexService.DescriptionIndex);
        var sku = XlAdapter.ExtractInteger(row, IndexService.ItemNumberIndex);
        var vndr = XlAdapter.ExtractString(row, IndexService.VendorIndex);
        var pq = XlAdapter.ExtractString(row, IndexService.PalletQtyIndex);
        var upc = XlAdapter.ExtractString(row, IndexService.BarcodeIndex);

        yield return new PatchRow(
          patchName, 
          row, 
          sec, 
          addr, 
          sku, 
          vndr, 
          desc, 
          pq, 
          upc);
      }
    }
  }
}