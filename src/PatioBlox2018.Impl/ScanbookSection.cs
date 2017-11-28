namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Threading;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookSection : ScanbookEntityBase<ScanbookBook, ScanbookPage>
  {
    private static TextInfo TextInfo { get; }

    static ScanbookSection()
    {
      TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
    }

    public ScanbookSection(IPatchRow sectionRow, Func<int, ScanbookBook> parentFinder)
      : base(sectionRow, parentFinder)
    {
      Book = parentFinder(SourceRowIndex);
    }

    public ScanbookBook Book { get; }

    [JsonProperty(PropertyName = "pages")]
    public IEnumerable<ScanbookPage> Pages => Children.AsEnumerable();

    public string Name =>
      TextInfo.ToTitleCase(PatchRow.Section.ToLower());

    [JsonIgnore]
    public int PageCount => Children.Count;

  }
}