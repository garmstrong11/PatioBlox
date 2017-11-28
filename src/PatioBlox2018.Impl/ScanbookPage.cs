namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPage : ScanbookEntityBase<ScanbookSection, ScanbookPatioBlok>
  {
    public ScanbookPage(IPatchRow pageRow, Func<int, ScanbookSection> parentFinder)
      : base(pageRow, parentFinder)
    {
      Section = parentFinder(SourceRowIndex);
      Section.AddChild(this);
    }

    public ScanbookSection Section { get; }

    [JsonProperty]
    public string Header => Section.Name;

    [JsonProperty(PropertyName = "blocks")]
    public IEnumerable<ScanbookPatioBlok> PatioBlox => Children.AsEnumerable();

    public int BlockCount => Children.Count;
  }
}