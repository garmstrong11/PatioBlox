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
  }
}