namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public static class BookBuilder
  {
    private static Regex PageRegex { get; }

    static BookBuilder() =>
      PageRegex = new Regex(@"^Page", RegexOptions.Compiled);

    public static IBook BuildBook(string patchName, IJob job, IEnumerable<IPatchRow> patchRows)
    {
      if (job == null) throw new ArgumentNullException(nameof(job));
      if (patchRows == null) throw new ArgumentNullException(nameof(patchRows));

      if (string.IsNullOrWhiteSpace(patchName))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchName));

      var rows = patchRows.ToList();
      if (!rows.Any())
        throw new ArgumentException("Incoming patch row sequence is empty.", nameof(patchRows));

      var book = new ScanbookBook(patchName, job);

      var sectionContentRows = rows
        .Where(p => !string.IsNullOrWhiteSpace(p.Section))
        .ToList();

      var pageRows = sectionContentRows.Where(p => PageRegex.IsMatch(p.Section)).ToList();
      var blokRows = rows.Where(p => p.ItemNumber.HasValue);

      var sections = sectionContentRows
        .Except(pageRows)
        .Select(s => new ScanbookSection(s, book))
        .ToList();

      var pages = pageRows
        .Select(pr => new ScanbookPage(pr, sections))
        .ToList();

      var unused = blokRows.Select(b => new ScanbookPatioBlok(b, pages)).ToList();

      return book;
    }
  }
}