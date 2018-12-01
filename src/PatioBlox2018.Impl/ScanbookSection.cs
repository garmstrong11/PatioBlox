namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Globalization;
  using System.Linq;
  using System.Threading;
  using MoreLinq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookSection
  {
    private static TextInfo TextInfo { get; }
    private static int BatchSize { get; }

    private IPatchRow SectionRow { get; }
    private List<IPatchRow> BlockRows { get; }

    static ScanbookSection()
    {
      TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
      BatchSize = int.Parse(ConfigurationManager.AppSettings["CellsPerPage"]);
    }

    public ScanbookSection(IPatchRow sectionRow)
    {
      SectionRow = sectionRow ?? throw new ArgumentNullException(nameof(sectionRow));
      BlockRows = new List<IPatchRow>();
    }

    public void AddBlockRow(IPatchRow blockRow) => BlockRows.Add(blockRow);
    public int SourceRowIndex => SectionRow.SourceRowIndex;

    [JsonProperty(PropertyName = "pages")]
    public IEnumerable<ScanbookPage> Pages => 
      BlockRows.Batch(BatchSize)
        .Select(b => new ScanbookPage(b, this));

    public string Name =>
      TextInfo.ToTitleCase(SectionRow.Section.ToLower());

    [JsonIgnore]
    public int PageCount
    {
      get
      {
        var cnt = Pages.Count();
        return cnt % 2 == 0 ? cnt : cnt + 1;
      }
    }

  }
}