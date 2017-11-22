namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookSection : ISection
  {
    private List<IPage> PageList { get; }
    private IPatchRow SectionRow { get; }

    public ScanbookSection(IPatchRow sectionRow, IEnumerable<IBook> books)
    {
      SectionRow = sectionRow ?? throw new ArgumentNullException(nameof(sectionRow));
      Book = books?.FirstOrDefault(b => b.Name == SectionRow.PatchName) ??
             throw new ArgumentException($"No book found with the name '{SectionRow.PatchName}'.", nameof(books));

      PageList = new List<IPage>();
    }
    
    [JsonIgnore]
    public int SourceRowIndex => SectionRow.SourceRowIndex;

    public void AddPage(IPage page) => PageList.Add(page);

    public IEnumerable<IPage> Pages => PageList.OrderBy(p => p.SourceRowIndex).AsEnumerable();

    [JsonIgnore]
    public int PageCount => Pages.Count();

    public string Name => SectionRow.Section;

    [JsonIgnore]
    public IBook Book { get; }
  }
}