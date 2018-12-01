namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPage
  {
    private IEnumerable<IPatchRow> BlockRows { get; }

    public ScanbookPage(IEnumerable<IPatchRow> blockRows, string header)
    {
      BlockRows = blockRows;
      Header = header;
    }

    [JsonProperty]
    public string Header { get; }

    [JsonProperty(PropertyName = "blocks")]
    public IEnumerable<ScanbookPatioBlock> PatioBlox =>
      BlockRows.Select(b => new ScanbookPatioBlock(b));

    public int BlockCount => BlockRows.Count();
  }
}