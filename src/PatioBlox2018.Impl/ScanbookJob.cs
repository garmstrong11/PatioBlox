namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookJob : IJob
  {
    private List<string> SourcePaths { get; }
    private List<IBook> BookList { get; }
    private IExtractor<IPatchRow> Extractor { get; }

    public ScanbookJob(IExtractor<IPatchRow> extractor)
    {
      Extractor = extractor ?? throw new ArgumentNullException(nameof(extractor));

      SourcePaths = new List<string>();
      BookList = new List<IBook>();
    }

    public void AddBook(IBook book) => BookList.Add(book);

    public string Name => ConfigurationManager.AppSettings["JobName"];

    public void BuildBooks()
    {
      var extracts = Extractor.Extract(SourcePaths).ToList();
      var patchNames = extracts
        .Select(e => e.PatchName)
        .Distinct();

      foreach (var patchName in patchNames) {
        var patchRows = extracts
          .Where(e => e.PatchName.Equals(patchName, StringComparison.CurrentCultureIgnoreCase));

        BookList.Add(BookBuilder.BuildBook(patchName, this, patchRows));
      }
    }

    public string GetJson()
    {
      return "Hello from Jason!";
    }

    public IDictionary<string, IBook> Books =>
      BookList.OrderBy(b => b.Name).ToDictionary(k => k.Name);

    [JsonIgnore]
    public IEnumerable<string> DataSourcePaths => SourcePaths.AsEnumerable();

    public void AddDataSource(string dataSourcePath) 
      => SourcePaths.Add(dataSourcePath);

    [JsonIgnore]
    public int PageCount => BookList.Sum(b => b.PageCount);
  }
}