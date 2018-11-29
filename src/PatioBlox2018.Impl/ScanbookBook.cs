namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookBook //: IBook
  {
    private static Regex PageRegex { get; }
    private IAdvertisingPatch AdPatch { get; }

    static ScanbookBook()
    {
      PageRegex = new Regex(@"^[Pp]age", RegexOptions.Compiled);
    }

    public ScanbookBook(
      IAdvertisingPatch adPatch, 
      ScanbookJob job, 
      IEnumerable<IPatchRow> patchRows,
      IDictionary<string, IBarcode> barcodeMap)
    {
      AdPatch = adPatch ?? throw new ArgumentNullException(nameof(adPatch));
      Job = job ?? throw new ArgumentNullException(nameof(job));

      var rows = patchRows.ToList();
      if (!rows.Any())
        throw new ArgumentException("Incoming patch row sequence is empty.", nameof(patchRows));

      var patioBlockRows = rows.Where(p => p.ItemNumber.HasValue).ToList();
      var sectionContentRows = rows.Where(p => !string.IsNullOrWhiteSpace(p.Section)).ToList();
      var pageRows = sectionContentRows.Where(p => PageRegex.IsMatch(p.Section)).ToList();

      SectionSet = new SortedSet<ScanbookSection>(
        sectionContentRows
          .Except(pageRows)
          .Select(s => new ScanbookSection(s, FindParentBook)));

      PatioBlocks = new List<ScanbookPatioBlok>(
        patioBlockRows
        .Select(b => new ScanbookPatioBlok(
            b, 
            FindParentSection, 
            barcodeMap[b.Upc])));

      Pages = SectionSet.SelectMany(s => s.Pages).ToList();
    }

    private SortedSet<ScanbookSection> SectionSet { get; }

    [JsonProperty]
    public List<ScanbookPage> Pages { get; }
    public List<ScanbookPatioBlok> PatioBlocks { get; }

    private ScanbookBook FindParentBook(int sourceRowIndex) => this;

    private ScanbookSection FindParentSection(int sourceRowIndex)
      => SectionSet.Last(s => s.SourceRowIndex <= sourceRowIndex);

    public List<string> DuplicatePatioBlocks =>
      PatioBlocks
        .GroupBy(b => b.ItemNumber)
        .Where(g => g.Count() > 1)
        .Select(g =>
          $"Item {g.Key} appears multiple times on patch {Name} in lines {string.Join(", ", g.Select(r => r.SourceRowIndex))}")
        .ToList();

    public bool HasDuplicateRows => DuplicatePatioBlocks.Any();

    private int TotalPages => Pages.Count;

    public static int CopiesPerStore => int.Parse(ConfigurationManager.AppSettings["CopiesPerStore"]);
    public int StoreCount => AdPatch.StoreCount;
    public int PageCount => TotalPages + (TotalPages % 2 == 0 ? 0 : 1);
    public int SheetCount => PageCount / 2;
    public int SetsForPatch => StoreCount * CopiesPerStore;

    public ScanbookJob Job { get; }

    [JsonProperty]
    public string Name => AdPatch.Name;

    public override string ToString() => $"Book {Name}";
    public int GetBlockCount() => PatioBlocks.Count;

    public IEnumerable<string> GetSheetNames()
    {
      for (var i = 1; i < PageCount; i += 2) {
        yield return $"{Name}_{i:D2}-{i + 1:D2}";
      }
    }
  }
}