namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using System.Text.RegularExpressions;
  using MoreLinq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookBook
  {
    private static Regex PageRegex { get; }
    private static int BatchSize { get; }
    private IAdvertisingPatch AdPatch { get; }
    private IEnumerable<IGrouping<string, ScanbookPatioBlock>> Sections { get; }

    static ScanbookBook()
    {
      PageRegex = new Regex(@"^Pa?ge", RegexOptions.Compiled | RegexOptions.IgnoreCase);
      BatchSize = int.Parse(ConfigurationManager.AppSettings["CellsPerPage"]);
    }

    public ScanbookBook(IEnumerable<IPatchRow> patchRows, IAdvertisingPatch adPatch)
    {
      if (patchRows == null) throw new ArgumentNullException(nameof(patchRows));
      AdPatch = adPatch ?? throw new ArgumentNullException(nameof(adPatch));

      var rowList = patchRows.ToList();
      var blockRows =         rowList.Where(pr => pr.ItemNumber.HasValue);
      var sectionOrPageRows = rowList.Where(pr => !string.IsNullOrWhiteSpace(pr.Section));
      var pageRows =          rowList.Where(pr => PageRegex.IsMatch(pr.Section));

      var sectionRows = sectionOrPageRows.Except(pageRows);

      Sections = blockRows
        .Select(br => new ScanbookPatioBlock(
          br, 
          sectionRows.Last(s => s.SourceRowIndex <= br.SourceRowIndex).Section))
        .GroupBy(pr => pr.Section);
    }

    [JsonProperty(PropertyName = "pages", Order = 1)]
    public IEnumerable<ScanbookPage> Pages => 
      Sections
        .SelectMany(groop => groop.Batch(BatchSize).Select(b => new ScanbookPage(b, groop.Key)));

    [JsonProperty(PropertyName = "name", Order = 0)]
    public string Name => AdPatch.Name;

    public List<string> DuplicatePatioBlocks =>
      Pages.SelectMany(s => s.PatioBlocks)
        .GroupBy(b => b.ItemNumber)
        .Where(g => g.Count() > 1)
        .Select(g =>
          $"Item {g.Key} appears multiple times on patch {Name} in lines {string.Join(", ", g.Select(r => r.SourceRowIndex))}")
        .ToList();

    public bool HasDuplicateRows => DuplicatePatioBlocks.Any();

    private int TotalPages => Pages.Count();

    public static int CopiesPerStore => int.Parse(ConfigurationManager.AppSettings["CopiesPerStore"]);
    public int StoreCount => AdPatch.StoreCount;
    public int PageCount => TotalPages + (TotalPages % 2 == 0 ? 0 : 1);
    public int SheetCount => PageCount / 2;
    public int SetsForPatch => StoreCount * CopiesPerStore;


    public override string ToString() => $"Book {Name}";

    public int BlockCount =>
      Pages.SelectMany(p => p.PatioBlocks).Count();

    public IEnumerable<string> GetSheetNames()
    {
      for (var i = 1; i < PageCount; i += 2)
      {
        yield return $"{Name}_{i:D2}-{i + 1:D2}";
      }
    }
  }
}