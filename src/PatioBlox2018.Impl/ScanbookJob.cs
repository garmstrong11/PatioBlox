namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookJob : IJob
  {
    private List<string> SourcePaths { get; }
    private IExtractionResult Result { get; }
    private List<IPatchRow> PatchRows { get; }
    private List<IBook> BookList { get; }
    private IList<IPatchRow> PageRows { get; }
    private IList<IPatchRow> SectionRows { get; }
    private IList<IPatchRow> BlockRows { get; }
    private static Regex PageRegex { get; }

    static ScanbookJob() =>
      PageRegex = new Regex(@"^Page\s+(\d+)$", RegexOptions.Compiled);

    public ScanbookJob(IExtractionResult extractionResult)
    {
      Result = extractionResult ?? throw new ArgumentNullException(nameof(extractionResult));
      PatchRows = extractionResult.PatchRowExtracts.ToList();

      SourcePaths = new List<string>();
      BookList = new List<IBook>();

      var sectionContentRows = PatchRows.Where(p => !string.IsNullOrWhiteSpace(p.Section)).ToList();
      PageRows = sectionContentRows.Where(p => PageRegex.IsMatch(p.Section)).ToList();
      SectionRows = sectionContentRows.Except(PageRows).ToList();
      BlockRows = PatchRows.Where(p => p.ItemNumber.HasValue).ToList();
    }

    public void AddBook(IBook book) => BookList.Add(book);

    public string Name => ConfigurationManager.AppSettings["JobName"];

    public string GetJson()
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<IBook> Books => 
      BookList.OrderBy(b => b.Name).AsEnumerable();

    [JsonIgnore]
    public IEnumerable<string> DataSourcePaths => SourcePaths.AsEnumerable();

    public void AddDataSource(string dataSourcePath)
    {
      SourcePaths.Add(dataSourcePath);
    }

    [JsonIgnore]
    public int PageCount => Books.Sum(b => b.PageCount);
  }
}