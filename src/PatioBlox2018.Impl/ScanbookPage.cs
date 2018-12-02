namespace PatioBlox2018.Impl
{
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Threading;
  using Newtonsoft.Json;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookPage
  {
    private static TextInfo TextInfo { get; }

    static ScanbookPage()
    {
      TextInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
    }

    public ScanbookPage(IEnumerable<ScanbookPatioBlock> patioBlocks, string header)
    {
      PatioBlocks = patioBlocks;
      Header = TextInfo.ToTitleCase(header.ToLower());
    }

    [JsonProperty(PropertyName = "header", Order = 0)]
    public string Header { get; }

    [JsonProperty(PropertyName = "blocks", Order = 1)]
    public IEnumerable<ScanbookPatioBlock> PatioBlocks { get; }

    public int BlockCount => PatioBlocks.Count();
  }
}