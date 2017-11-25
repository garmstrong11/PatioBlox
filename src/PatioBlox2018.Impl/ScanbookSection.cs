namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Threading;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookSection : ISection
  {
    private List<IPage> PageList { get; }
    private IPatchRow SectionRow { get; }
    private static TextInfo TextInfo { get; }

    static ScanbookSection()
    {
      TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
    }

    public ScanbookSection(IPatchRow sectionRow, IBook book)
    {
      SectionRow = sectionRow ?? 
        throw new ArgumentNullException(nameof(sectionRow));
      Book = book ?? 
        throw new ArgumentNullException(nameof(book));

      Book.AddSection(this);
      PageList = new List<IPage>();
    }
    
    [JsonIgnore]
    public int SourceRowIndex => SectionRow.SourceRowIndex;

    public void AddPage(IPage page) => PageList.Add(page);

    public IEnumerable<IPage> Pages => 
      PageList.OrderBy(p => p.SourceRowIndex).AsEnumerable();

    [JsonIgnore]
    public int PageCount => Pages.Count();

    public string Name =>
        TextInfo.ToTitleCase(SectionRow.Section.ToLower());

    [JsonIgnore]
    public IBook Book { get; }
  }
}