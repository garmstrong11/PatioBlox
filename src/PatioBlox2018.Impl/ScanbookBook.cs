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

    static ScanbookBook() =>
      PageRegex = new Regex(@"^Page", RegexOptions.Compiled);

    public ScanbookBook(IAdvertisingPatch adPatch, ScanbookJob job, IEnumerable<IPatchRow> patchRows)
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

      PageSet = new SortedSet<ScanbookPage>(
        pageRows.Select(p => new ScanbookPage(p, FindParentSection)));

      BlockSet = new SortedSet<ScanbookPatioBlok>(
        patioBlockRows.Select(b => new ScanbookPatioBlok(b, FindParentPage)));
    }

    private SortedSet<ScanbookSection> SectionSet { get; }
    private SortedSet<ScanbookPage> PageSet { get; }
    private SortedSet<ScanbookPatioBlok> BlockSet { get; }

    private ScanbookBook FindParentBook(int sourceRowIndex) => this;

    private ScanbookSection FindParentSection(int sourceRowIndex)
      => SectionSet.Last(s => s.SourceRowIndex <= sourceRowIndex);

    private ScanbookPage FindParentPage(int sourceRowIndex)
      => PageSet.Last(p => p.SourceRowIndex <= sourceRowIndex);

    private static int CopiesPerStore => int.Parse(ConfigurationManager.AppSettings["CopiesPerStore"]);
    private int TotalPages => PageSet.Count;
    private int StoreCount => AdPatch.StoreCount;
    private int PrintedPages => PageCount * StoreCount * CopiesPerStore;
    private int Augmentor => TotalPages % 2 == 0 ? 0 : 1;
    public int PageCount => TotalPages + Augmentor;
    public int SheetCount => PageCount / 2;
    public int SetsForPatch => StoreCount * CopiesPerStore;

    public string EstimateRow =>
      $@"{Name},{PageCount},{SheetCount},{StoreCount},{CopiesPerStore},{PrintedPages},{SetsForPatch}";

    public IEnumerable<string> GetMetrixRows(string header)
    {
      var result = new List<string> {header};
      var rows = GetSheetNames()
        .Select(n => $@"{n},1,{SetsForPatch},8.5,5.5,,,{n}.pdf,,,,,,,,");
      result.AddRange(rows);

      return result.AsEnumerable();
    }

    public ScanbookJob Job { get; }

    [JsonProperty]
    public string Name => AdPatch.Name;

    [JsonProperty]
    public IEnumerable<ScanbookPage> Pages => PageSet.AsEnumerable();

    public override string ToString() => $"Book {Name}";
    public int GetBlockCount() => BlockSet.Count;

    private IEnumerable<string> GetSheetNames()
    {
      for (var i = 1; i < PageCount; i += 2) {
        yield return $"{Name}_{i:D2}-{i + 1:D2}";
      }
    }
  }
}