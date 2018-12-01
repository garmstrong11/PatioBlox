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
  public class ScanbookBook
  {
    private static Regex PageRegex { get; }
    private IAdvertisingPatch AdPatch { get; }
    internal IEnumerable<ScanbookSection> Sections { get; }

    static ScanbookBook() 
      => PageRegex = new Regex(@"^Pa?ge", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public ScanbookBook(IEnumerable<IPatchRow> patchRows, IAdvertisingPatch adPatch)
    {
      if (patchRows == null) throw new ArgumentNullException(nameof(patchRows));
      AdPatch = adPatch ?? throw new ArgumentNullException(nameof(adPatch));

      var rowList = patchRows.ToList();
      var blockRows = rowList
        .Where(pr => pr.ItemNumber.HasValue)
        .ToList();

      var sectionOrPageRows = rowList.Where(pr => !string.IsNullOrWhiteSpace(pr.Section));
      var pageRows = rowList.Where(pr => PageRegex.IsMatch(pr.Section));

      Sections = sectionOrPageRows.Except(pageRows).Select(s => new ScanbookSection(s));
      DistributeBlocks(blockRows);
    }

    private void DistributeBlocks(List<IPatchRow> blockRows)
    {
      blockRows
        .ForEach(block => Sections.Last(s => block.SourceRowIndex <= s.SourceRowIndex)
          .AddBlockRow(block));
    }

    //public List<string> DuplicatePatioBlocks =>
    //  PatioBlocks
    //    .GroupBy(b => b.ItemNumber)
    //    .Where(g => g.Count() > 1)
    //    .Select(g =>
    //      $"Item {g.Key} appears multiple times on patch {Name} in lines {string.Join(", ", g.Select(r => r.SourceRowIndex))}")
    //    .ToList();

    //public bool HasDuplicateRows => DuplicatePatioBlocks.Any();

    // private int TotalPages => Pages.Count;

    public static int CopiesPerStore => int.Parse(ConfigurationManager.AppSettings["CopiesPerStore"]);
    public int StoreCount => AdPatch.StoreCount;
    //public int PageCount => TotalPages + (TotalPages % 2 == 0 ? 0 : 1);
    //public int SheetCount => PageCount / 2;
    public int SetsForPatch => StoreCount * CopiesPerStore;

    public ScanbookJob Job { get; }

    [JsonProperty]
    public string Name => AdPatch.Name;

    public override string ToString() => $"Book {Name}";
    //public int GetBlockCount() => PatioBlocks.Count;

    //public IEnumerable<string> GetSheetNames()
    //{
    //  for (var i = 1; i < PageCount; i += 2) {
    //    yield return $"{Name}_{i:D2}-{i + 1:D2}";
    //  }
    //}
  }
}