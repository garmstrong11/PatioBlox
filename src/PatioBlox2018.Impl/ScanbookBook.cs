namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookBook : IBook
  {
    private List<ISection> SectionList { get; }

    public ScanbookBook(string patchName, IJob job)
    {
      Name = patchName ?? throw new ArgumentNullException(nameof(patchName));
      Job = job ?? throw new ArgumentNullException(nameof(job));

      SectionList = new List<ISection>();
    }

    public IJob Job { get; }

    public IEnumerable<ISection> Sections => 
      SectionList.OrderBy(s => s.SourceRowIndex).AsEnumerable();

    public string Name { get; }
    public void AddSection(ISection section) => SectionList.Add(section);

    [JsonIgnore]
    public int PageCount
    {
      get
      {
        var bookTotal = Sections.Sum(s => s.PageCount);
        return bookTotal + (bookTotal % 2 == 0 ? 0 : 1);
      }
    }

    public override string ToString() => $"Book name: {Name}";

    public IEnumerable<string> SheetNames => GetSheetNames();

    private IEnumerable<string> GetSheetNames()
    {
      for (var i = 1; i < PageCount; i += 2) {
        yield return $"{Name}_{i:D2}-{i + 1:D2}";
      }
    }
  }
}