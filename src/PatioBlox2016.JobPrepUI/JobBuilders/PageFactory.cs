namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;
  using Concrete;

  public class PageFactory : IPageFactory
  {
    private readonly ICellFactory _cellFactory;
    private readonly ISettingsService _settings;

    public PageFactory(ICellFactory cellFactory, ISettingsService settings)
    {
      _cellFactory = cellFactory;
      _settings = settings;
    }

    public IEnumerable<Page> CreatePages(Section section, IEnumerable<IPatchRowExtract> patchRows)
    {
      var buckets = SplitExtractsIntoPageBuckets(patchRows);
      foreach (var bucket in buckets) {
        var page = new Page(section);
        foreach (var patchRowExtract in bucket) {
          page.AddCell(_cellFactory.CreateCell(page, patchRowExtract));
        }

        yield return page;
        //section.AddPage(page);
      }
    }

    private IEnumerable<IEnumerable<IPatchRowExtract>> SplitExtractsIntoPageBuckets(IEnumerable<IPatchRowExtract> patchRows)
    {
      var extracts = patchRows.ToList();
      var cellsPerPage = _settings.CellsPerPage;

      //IPatchRowExtract [] bucket = null;
      List<IPatchRowExtract> bucket = null;

      var count = 0;

      foreach (var extract in extracts)
      {
        if (bucket == null)
        {
          bucket = new List<IPatchRowExtract>();
        }

        bucket.Add(extract);
        count++;

        // The bucket is fully populated before it's yielded
        if (count != cellsPerPage)
        {
          continue;
        }

        yield return bucket.AsEnumerable();

        bucket = null;
        count = 0;
      }

      // Yield the remaining cells 
      if (bucket != null && count > 0)
      {
        yield return bucket.AsEnumerable();
      }
    } 
  }
}