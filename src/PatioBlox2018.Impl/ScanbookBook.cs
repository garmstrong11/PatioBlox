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
  public class ScanbookBook : ScanbookEntityBase<ScanbookJob, ScanbookSection>
  {
    private static Regex PageRegex { get; }
    private IAdvertisingPatch AdPatch { get; }
    internal IEnumerable<ScanbookSection> Sections { get; }

    static ScanbookBook()
    {
      PageRegex = new Regex(@"^[Pp]age", RegexOptions.Compiled);
    }

    public ScanbookBook(IEnumerable<IPatchRow> patchRows, Func<int, ScanbookJob> parentFinder)
      : base(patchRows, parentFinder)
    {
      var sectionContentRows = PatchRows.Where(p => !string.IsNullOrWhiteSpace(p.Section)).ToList();
      var pageRows = sectionContentRows.Where(p => PageRegex.IsMatch(p.Section)).ToList();

      Sections =
        sectionContentRows
          .Except(pageRows)
          .Select(s => new ScanbookSection(PatchRows, FindParentBookForSection, s.SourceRowIndex));
    }

    private ScanbookBook FindParentBookForSection(int childIndex)
    {
      return this;
    }

    //private ScanbookBook FindParentBook(int sourceRowIndex) => this;

    //private ScanbookSection FindParentSection(int sourceRowIndex)
    //  => SectionSet.Last(s => s.SourceRowIndex <= sourceRowIndex);

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