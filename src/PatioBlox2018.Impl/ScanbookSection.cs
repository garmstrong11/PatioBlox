namespace PatioBlox2018.Impl
{
  public class ScanbookSection
  {
    //private static TextInfo TextInfo { get; }
    //private static int BatchSize { get; }

    //private IPatchRow SectionRow { get; }
    //private List<IPatchRow> BlockRows { get; }

    //static ScanbookSection()
    //{
    //  TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
    //  BatchSize = int.Parse(ConfigurationManager.AppSettings["CellsPerPage"]);
    //}

    //public ScanbookSection(IPatchRow sectionRow)
    //{
    //  SectionRow = sectionRow ?? throw new ArgumentNullException(nameof(sectionRow));
    //  BlockRows = new List<IPatchRow>();
    //}

    //public void AddBlockRow(IPatchRow blockRow) => BlockRows.Add(blockRow);
    //public int SourceRowIndex => SectionRow.SourceRowIndex;

    //public IEnumerable<ScanbookPage> Pages => 
    //  BlockRows.Batch(BatchSize)
    //    .Select(b => new ScanbookPage(b, SectionRow.Section));

    //public string Name =>
    //  TextInfo.ToTitleCase(SectionRow.Section.ToLower());

    //public JObject JObject =>
    //  new JObject(
    //    new JProperty("pages",
    //      new JArray(Pages.Select(p => p.JObject))));

    //public int PageCount
    //{
    //  get
    //  {
    //    var cnt = Pages.Count();
    //    return cnt % 2 == 0 ? cnt : cnt + 1;
    //  }
    //}

  }
}