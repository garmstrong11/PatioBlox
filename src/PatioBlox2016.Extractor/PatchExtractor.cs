namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.IO.Abstractions;
  using System.Linq;
  using Abstract;

  public class PatchExtractor : ExtractorBase<IPatchRowExtract>, IPatchExtractor
  {
    private readonly IColumnIndexService _indexService;

    public PatchExtractor(IDataSourceAdapter adapter, IFileSystem fileSystem, 
      IColumnIndexService indexService) 
      : base(adapter, fileSystem)
    {
      _indexService = indexService;
    }

    public override IEnumerable<IPatchRowExtract> Extract()
    {
      var patchNames = ExtractPatchNames();
      var result = new List<IPatchRowExtract>();

      foreach (var name in patchNames) {
        ChangeCurrentPatch(name);
        result.AddRange(ExtractOnePatch());
      }

      return result.AsEnumerable();
    }

    private IEnumerable<IPatchRowExtract> ExtractOnePatch()
    {
      var rowCount = XlAdapter.RowCount;
      var startRowIndex = FindHeaderRow() + 1;

      for (var row = startRowIndex; row <= rowCount; row++) {
        var extract = new PatchRowExtract(XlAdapter.SheetName, row)
                      {
                        Section = XlAdapter.ExtractString(row,_indexService.SectionIndex),
                        Sku = XlAdapter.ExtractInteger(row, _indexService.ItemIndex),
                        Description = XlAdapter.ExtractString(row, _indexService.DescriptionIndex),
                        PalletQuanity = XlAdapter.ExtractString(row, _indexService.PalletQtyIndex),
                        Upc = XlAdapter.ExtractString(row, _indexService.UpcIndex)
                      };

        yield return extract;
      }
    }

    public IEnumerable<string> ExtractPatchNames()
    {
      return XlAdapter.GetSheetNames();
    }

    public int FindHeaderRow()
    {
      var rowCount = XlAdapter.RowCount;
      for (var rowIndex = 1; rowIndex <= rowCount; rowIndex++) {
        var content = XlAdapter.ExtractRawString(rowIndex, _indexService.ItemIndex);
        if (content == "Item #") return rowIndex;
      }

      throw new InvalidOperationException("Unable to find start row.");
    }

    public void ChangeCurrentPatch(string patchName)
    {
      XlAdapter.SetActiveSheetByName(patchName);
    }
  }
}