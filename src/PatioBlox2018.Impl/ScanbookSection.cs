namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Globalization;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading;
  using MoreLinq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookSection : ScanbookEntityBase<ScanbookBook, ScanbookPage>
  {
    private static TextInfo TextInfo { get; }
    private static int BatchSize { get; }
    private static Regex PageRegex { get; }

    static ScanbookSection()
    {
      TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
      BatchSize = int.Parse(ConfigurationManager.AppSettings["CellsPerPage"]);
      PageRegex = new Regex(@"^[Pp]age", RegexOptions.Compiled);
    }

    public ScanbookSection(IEnumerable<IPatchRow> patchRows, Func<int, ScanbookBook> parentFinder, int sourceRowIndex)
      : base(patchRows, parentFinder)
    {
      SourceRowIndex = sourceRowIndex;
    }

    public int SourceRowIndex { get; }

    private ScanbookSection FindParentSectionForPage(int pagePatchIndex) 
      => Parent.Sections.Last(s => s.SourceRowIndex < pagePatchIndex);

    [JsonProperty(PropertyName = "pages")]
    public IEnumerable<ScanbookPage> Pages => 
      Children.Batch(BatchSize, b => new ScanbookPage(b.ToList(), this));

    public string Name =>
      TextInfo.ToTitleCase(StartPatchRow.Section.ToLower());

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