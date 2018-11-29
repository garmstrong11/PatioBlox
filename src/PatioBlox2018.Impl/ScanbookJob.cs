namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using MoreLinq;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl.Barcodes;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookJob
  {
    private List<ScanbookBook> BookList { get; }
    private IEnumerable<IPatchRow> PatchRows { get; }
    private IEnumerable<IAdvertisingPatch> Stores { get; }
    public IDictionary<string, IBarcode> BarcodeMap { get; }

    public ScanbookJob(
      IExtractor<IPatchRow> patchExtractor, 
      IExtractor<IAdvertisingPatch> adPatchExtractor, 
      IBarcodeFactory barcodeFactory)
    {
      if (patchExtractor == null) throw new ArgumentNullException(nameof(patchExtractor));
      if (adPatchExtractor == null) throw new ArgumentNullException(nameof(adPatchExtractor));
      if (barcodeFactory == null) throw new ArgumentNullException(nameof(barcodeFactory));

      PatchRows = patchExtractor
        .Extract(ScanbookFileOps.PatioBloxExcelFilePath);
      Stores = adPatchExtractor.Extract(ScanbookFileOps.StoreListExcelFilePath);
      BookList = new List<ScanbookBook>();

      BarcodeMap = PatchRows
        .Select(barcodeFactory.Create)
        .Distinct(new BarcodeCandidateEqualityComparer())
        .ToDictionary(k => k.Candidate, StringComparer.CurrentCulture);
      BarcodeMap.Add(string.Empty, new NullBarcode());
    }

    public string Name => ConfigurationManager.AppSettings["JobName"];

    public void BuildBooks(string blockPath, string storePath, IBarcodeFactory factory)
    {
      var blocks = PatchRows.ToLookup(k => k.PatchName);
      var stores = Stores.ToDictionary(k => k.Name);

      var missingStores = blocks.Select(b => b.Key).Except(stores.Keys).AsQueryable();

      if (missingStores.Any())
        throw new InvalidOperationException(
          $"Some patches have no store data ({string.Join(", ", missingStores)}).");

      BookList.AddRange(blocks.Select(b => new ScanbookBook(stores[b.Key], this, b, BarcodeMap)));
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

    public IEnumerable<string> ItemSectionWarnings => BookList
      .SelectMany(b => b.PatioBlocks)
      .DistinctBy(k => new {k.ItemNumber, k.Section.Name})
      .ToLookup(k => k.ItemNumber, v => v)
      .Where(k => k.Count() > 1)
      .SelectMany(v => v.Select(k => $"Item {k.ItemNumber} appears in section {k.Section.Name} on patch {k.PatchName}"));

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

    public IEnumerable<string> MissingPhotos =>
      PatchRows
        .Select(p => $"{p.ItemNumber}.psd")
        .Except(ScanbookFileOps.PhotoFilenames)
        .Select(p => $"A photo for item {p} could not be found");

    public List<string> BarcodeErrors =>
      BarcodeMap
        .Values
        .Where(b => b.GetType() != typeof(ValidBarcode))
        .Select(b => $"{b.Value} used in patches {string.Join(", ", b.Usages)}")
        .ToList();

    public List<string> MissingUpcs => PatchRows
      .Where(pr => pr.ItemNumber.HasValue && string.IsNullOrEmpty(pr.Upc))
      .Select(pr => new {ItemId = pr.ItemNumber.Value, pr.PatchName})
      .GroupBy(b => b.ItemId)
      .Select(b => $"Item {b.Key} has no barcode in patch(es) {string.Join(", ", b.Select(a => a.PatchName))}")
      .ToList();

    public List<string> DuplicateItems =>
      BookList
        .Where(ck => ck.HasDuplicateRows)
        .SelectMany(bk => bk.DuplicatePatioBlocks)
        .ToList();
  }
}