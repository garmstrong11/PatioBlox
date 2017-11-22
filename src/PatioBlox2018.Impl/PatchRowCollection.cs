namespace PatioBlox2018.Impl
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class PatchRowCollection
  {
    private List<IPatchRow> PatchRows { get; }
    private static Regex PageRegex { get; }

    static PatchRowCollection() =>
      PageRegex = new Regex(@"^Page\s+(\d+)$", RegexOptions.Compiled);

    public PatchRowCollection(IEnumerable<IPatchRow> patchRows)
    {
      PatchRows = patchRows.ToList();
    }

    private IEnumerable<IPatchRow> SectionContentRows =>
      PatchRows.Where(p => !string.IsNullOrWhiteSpace(p.Section));

    public IEnumerable<IPatchRow> PageRows =>
      PatchRows.Where(p => PageRegex.IsMatch(p.Section));

    public IEnumerable<IPatchRow> SectionRows =>
      SectionContentRows.Except(PageRows);

    public IEnumerable<IPatchRow> BlockRows =>
      PatchRows.Where(p => p.ItemNumber.HasValue);

    public IEnumerable<ISection> GetBook(string patchName)
    {
      var secs = from sec in SectionRows
                 where
    }

  }
}