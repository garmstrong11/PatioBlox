namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.IO.Abstractions;
  using System.Linq;
  using Abstract;

  public class PatchExtractor : ExtractorBase<IPatchRowExtract>, IPatchExtractor
  {
    private readonly IColumnIndexService _indexService;
	  private readonly IEnumerable<string> _patchNames;
	  private readonly string _excelFileName;

    public PatchExtractor(IDataSourceAdapter adapter, IFileSystem fileSystem, 
      IColumnIndexService indexService) 
      : base(adapter, fileSystem)
    {
      _indexService = indexService;
	    _patchNames = ExtractPatchNames();
	    _excelFileName = Path.GetFileName(SourcePath);
    }

    public override IEnumerable<IPatchRowExtract> Extract()
    {
      var result = new List<IPatchRowExtract>();

      foreach (var name in _patchNames) {
        result.AddRange(ExtractOnePatch(name));
      }

      return result.AsEnumerable();
    }

    public IEnumerable<IPatchRowExtract> ExtractOnePatch(string patchName)
    {
	    if (string.IsNullOrWhiteSpace(patchName)) throw new ArgumentNullException("patchName");

	    if (!_patchNames.Contains(patchName)) {
		    throw new InvalidOperationException(
					string.Format("Patch {0} does not exist in the file {1}", patchName, _excelFileName));
	    }

			XlAdapter.SetActiveSheetByName(patchName);

	    var rowCount = XlAdapter.RowCount;
      var startRowIndex = FindHeaderRow() + 1;

      for (var row = startRowIndex; row <= rowCount; row++) {
        var sec = XlAdapter.ExtractString(row, _indexService.SectionIndex);
        var desc = XlAdapter.ExtractString(row, _indexService.DescriptionIndex);
        var sku = XlAdapter.ExtractInteger(row, _indexService.ItemIndex);
        var pq = XlAdapter.ExtractString(row, _indexService.PalletQtyIndex) ?? "00";

        var upc = XlAdapter.ExtractString(row, _indexService.UpcIndex)
                  ?? string.Format("{0}_{1}", sku, pq);

        var extract = new PatchRowExtract(patchName, row)
                      {
                        Section = sec,
                        Sku = sku,
                        Description = desc,
                        PalletQuanity = pq,
                        Upc = upc
                      };

        yield return extract;
      }
    }

    public IEnumerable<string> ExtractPatchNames()
    {
      return XlAdapter.GetSheetNames();
    }

    private int FindHeaderRow()
    {
      var rowCount = XlAdapter.RowCount;
      int rowIndex;

      for (rowIndex = 1; rowIndex <= rowCount; rowIndex++) {
        var content = XlAdapter.ExtractRawString(rowIndex, _indexService.ItemIndex);
        if (content == "Item #") return rowIndex;
      }

      var headerException = new PatioBloxHeaderExtractionException(XlAdapter.SheetName, SourcePath);
      throw headerException;
    }
  }
}