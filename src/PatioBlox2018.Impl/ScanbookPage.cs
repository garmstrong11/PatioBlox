namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookPage : IPage
  {
    private IPatchRow PageRow { get; }
    private List<IPatioBlok> BlokList { get; }

    public ScanbookPage(IPatchRow pageRow, IEnumerable<ISection> sections)
    {
      PageRow = pageRow ?? throw new ArgumentNullException(nameof(pageRow));

      Section = sections?.Last(sec => sec.SourceRowIndex < PageRow.SourceRowIndex) 
        ?? throw new ArgumentNullException(nameof(sections));

      Section.AddPage(this);
      BlokList = new List<IPatioBlok>();
    }

    [JsonIgnore]
    public ISection Section { get; }

    public string Header => Section.Name;

    public IEnumerable<IPatioBlok> PatioBlox => 
      BlokList.OrderBy(b => b.SourceRowIndex).AsEnumerable();

    [JsonIgnore]
    public int BlockCount => BlokList.Count;

    [JsonIgnore]
    public int SourceRowIndex => PageRow.SourceRowIndex;

    public void AddPatioBlok(IPatioBlok patioBlok) => BlokList.Add(patioBlok);
  }
}