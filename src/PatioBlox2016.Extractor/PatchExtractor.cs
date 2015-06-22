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
        var extract = new PatchRowExtract(patchName, row)
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

    private int FindHeaderRow()
    {
      var rowCount = XlAdapter.RowCount;
      for (var rowIndex = 1; rowIndex <= rowCount; rowIndex++) {
        var content = XlAdapter.ExtractRawString(rowIndex, _indexService.ItemIndex);
        if (content == "Item #") return rowIndex;
      }

      throw new InvalidOperationException(
				string.Format("Unable to find a header row on sheet {0} of file {1}.", XlAdapter.SheetName, _excelFileName));
    }
  }
}