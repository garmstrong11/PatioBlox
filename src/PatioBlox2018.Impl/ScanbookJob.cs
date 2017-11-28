namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.IO;
  using System.Linq;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookJob
  {
    private List<ScanbookBook> BookList { get; }
    private IFileOps FileOps { get; }
    private IExtractor<IPatchRow> BlockExtractor { get; }
    private IExtractor<IAdvertisingPatch> StoreExtractor { get; }

    public ScanbookJob(
      IExtractor<IPatchRow> blockExtractor, 
      IExtractor<IAdvertisingPatch> adPatchExtractor,
      IFileOps fileOps)
    {
      BlockExtractor = blockExtractor ?? throw new ArgumentNullException(nameof(blockExtractor));
      StoreExtractor = adPatchExtractor ?? throw new ArgumentNullException(nameof(adPatchExtractor));

      BookList = new List<ScanbookBook>();
    }

    public string Name => ConfigurationManager.AppSettings["JobName"];

    public void BuildBooks(string blockPath, string storePath)
    {
      var blocks = BlockExtractor.Extract(blockPath).ToLookup(k => k.PatchName);
      var stores = StoreExtractor.Extract(storePath).ToDictionary(k => k.Name);

      var missingStores = blocks.Select(b => b.Key).Except(stores.Keys).AsQueryable();

      if (missingStores.Any())
        throw new InvalidOperationException(
          $"Some patches have no store data ({string.Join(", ", missingStores)}).");

      BookList.AddRange(blocks.Select(b => new ScanbookBook(stores[b.Key], this, b)));
    }

    public string GetJson()
    {
      var resolver = new DefaultContractResolver
      {
        NamingStrategy = new CamelCaseNamingStrategy()
      };

      var settings = new JsonSerializerSettings
      {
        ContractResolver = resolver,
        Formatting = Formatting.Indented,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
      };

      return JsonConvert.SerializeObject(this, settings);
    }

    [JsonProperty(PropertyName = "patches")]
    public IDictionary<string, ScanbookBook> Books =>
      BookList.OrderBy(b => b.Name).ToDictionary(k => k.Name);

    public int PageCount => BookList.Sum(b => b.PageCount);
  }
}