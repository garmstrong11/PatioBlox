namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookJob : IJob
  {
    private IFileOps FileOps { get; }
    private List<ScanbookBook> BookList { get; }
    private IExtractor<IPatchRow> BlockExtractor { get; }
    private IBarcodeFactory BarcodeFactory { get; }
    private IExtractor<IAdvertisingPatch> StoreExtractor { get; }

    public ScanbookJob(
      IExtractor<IPatchRow> blockExtractor, 
      IExtractor<IAdvertisingPatch> adPatchExtractor,
      IBarcodeFactory barcodeFactory, 
      IFileOps fileOps)
    {
      FileOps = fileOps ?? throw new ArgumentNullException(nameof(fileOps));
      BarcodeFactory = barcodeFactory ?? throw new ArgumentNullException(nameof(barcodeFactory));
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

      BookList.AddRange(blocks.Select(b => new ScanbookBook(stores[b.Key], this, b, BarcodeFactory)));
    }

    private string GetJson()
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

    public void EmitPatchDataFile()
    {
      FileOps.EmitJsonDataFile(GetJsxBlocks());
    }

    //private IEnumerable<string> UniqueUpcs =>
    //  (
    //  from book in BookList
    //  from page in book.Pages
    //  from block in page.PatioBlox
    //  where block.Barcode.IsValid
    //  select block.Barcode.Value
    //  )
    //  .Distinct();

    public string GetJsxBlocks()
    {
      // Alter the JSON header to obtain a javascript IIFE:
      var json = GetJson().Replace("{\r\n  \"patches\": ", "(function () {\r\n patches = ");

      // Append the invocation parens:
      return $"{json}) ();";
    }

    [JsonProperty(PropertyName = "patches")]
    public IDictionary<string, ScanbookBook> Books =>
      BookList.OrderBy(b => b.Name).ToDictionary(k => k.Name);

    public int PageCount => BookList.Sum(b => b.PageCount);
  }
}