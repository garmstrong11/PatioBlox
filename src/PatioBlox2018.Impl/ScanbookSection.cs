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

  public class ScanbookSection : ScanbookEntityBase<ScanbookBook, ScanbookPage>
  {
    private static TextInfo TextInfo { get; }
    private static int BatchSize { get; }
    private IEnumerable<ScanbookPatioBlok> PatioBlocks { get; }

    static ScanbookSection()
    {
      TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
      BatchSize = int.Parse(ConfigurationManager.AppSettings["CellsPerPage"]);
    }

    public ScanbookSection(IPatchRow sectionRow, Func<int, ScanbookBook> parentFinder)
      : base(sectionRow, parentFinder)
    {
      Book = parentFinder(SourceRowIndex);
      PatioBlocks = Book.PatioBlocks.Where(pb => pb.Section.Name == Name);
    }

    /// <summary>
    /// The parent Book for this section
    /// </summary>
    public ScanbookBook Book { get; }

    [JsonProperty(PropertyName = "pages")]
    public IEnumerable<ScanbookPage> Pages => 
      Children.Batch(BatchSize, b => new ScanbookPage(b.ToList(), this));

    /// <summary>
    /// Projects the section's PatioBlock content into a sequence of pages
    /// </summary>
    /// <param name="blocks"></param>
    public void AddBlocks(IEnumerable<ScanbookPatioBlok> blocks)
    {
      foreach (var block in blocks.OrderBy(b => b.SourceRowIndex)) {
        Children.Add(block);
      }
    }

    public string Name =>
      TextInfo.ToTitleCase(PatchRow.Section.ToLower());

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