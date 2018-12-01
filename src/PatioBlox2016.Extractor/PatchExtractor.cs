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

      var result = new List<IPatchRow>();

      Initialize(excelFilePath);

      foreach (var name in XlAdapter.GetSheetNames()) {
        result.AddRange(ExtractOnePatch(name));
      }

      return result;
    }

    private IEnumerable<IPatchRow> ExtractOnePatch(string patchName)
    {
      if (string.IsNullOrWhiteSpace(patchName)) throw new ArgumentNullException(nameof(patchName));

      XlAdapter.SetActiveSheetByName(patchName);

      var rowCount = XlAdapter.RowCount;
      var startRowIndex = FindHeaderRow() + 1;

      for (var row = startRowIndex; row <= rowCount; row++)
      {
        var sec = XlAdapter.ExtractString(row, IndexService.SectionIndex);
        var addr = XlAdapter.ExtractInteger(row, IndexService.PageOrderIndex);
        var desc = XlAdapter.ExtractString(row, IndexService.DescriptionIndex);
        var sku = XlAdapter.ExtractInteger(row, IndexService.ItemNumberIndex);
        var vndr = XlAdapter.ExtractString(row, IndexService.VendorIndex);
        var pq = XlAdapter.ExtractString(row, IndexService.PalletQtyIndex);
        var upc = XlAdapter.ExtractString(row, IndexService.BarcodeIndex);

        yield return new PatchRow(patchName, row, sec, addr, sku, vndr, desc, pq, upc);
      }
    }

    private int FindHeaderRow()
    {
      var rowCount = XlAdapter.RowCount;
      int rowIndex;

      for (rowIndex = 1; rowIndex <= rowCount; rowIndex++)
      {
        var content = XlAdapter.ExtractRawString(rowIndex, IndexService.ItemNumberIndex);
        if (content.Equals("Item #", StringComparison.CurrentCulture)) return rowIndex;
      }

      var headerException = new PatioBloxHeaderExtractionException(XlAdapter.SheetName, SourcePath);
      throw headerException;
    }
  }
}