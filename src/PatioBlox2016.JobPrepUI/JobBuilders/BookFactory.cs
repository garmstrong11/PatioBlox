namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Abstract;
  using Concrete;
  using Concrete.Exceptions;
  using PatioBlox2016.DataAccess;

  public class BookFactory : IBookFactory
  {
    private readonly ICellFactory _cellFactory;
    private readonly ISettingsService _settings;
    private readonly PatioBloxContext _context;

    public BookFactory(ICellFactory cellFactory, ISettingsService settings, PatioBloxContext context)
    {
      _cellFactory = cellFactory;
      _settings = settings;
      _context = context;
    }

    public Book CreateBook(Job job, string name, IEnumerable<IPatchRowExtract> patchRows)
    {
      if (job == null) {throw new ArgumentNullException("job");}
      if (patchRows == null) {throw new ArgumentNullException("patchRows");}
      if (string.IsNullOrWhiteSpace(name)) {throw new ArgumentNullException("name");}

      var extracts = patchRows.ToList();

      if (!extracts.Any()) {
        throw new ArgumentException(string.Format("No PatchRows found for book \"{0}\".", name));
      }

      var book = new Book(job, name);

      var sections = ExtractSections(book, extracts);

      IEnumerable<Cell> cells;

      var descriptionDict = _context.Descriptions.Local.ToDictionary(k => k.Text, v => v.Id);
      var upcDict = _context.UpcReplacements.ToDictionary(k => k.InvalidUpc, v => v.Replacement);

      try {
        // Filter patchRows with Sku = -1
        cells = extracts
          .Where(e => e.Sku >= 0)
          .Select(pr => _cellFactory.CreateCell(pr, descriptionDict, upcDict))
          .ToList();
      }

      catch (CellConstructionException exc) {
        var message = string.Format("Cell construction failed for row {0} (Sku {1}) in book '{2}'",
          exc.RowIndex, exc.Sku, book.BookName);

        var bookException = new BookConstructionException(book.BookName, message);
        throw bookException;
      }

      foreach (var cell in cells) {
        cell.FindSection(sections);
      }

      foreach (var section in sections) {
        section.BuildPages();
      }

      // Filter empty sections and add to book:
      book.AddSectionRange(sections.Where(s => s.Cells.Any()));
      book.SetPageIndices();

      return book;
    }

    private List<Section> ExtractSections(Book book, IEnumerable<IPatchRowExtract> extracts)
    {
      var sectionsSet = new HashSet<Section>(new SectionNameEqualityComparer());

      var sections = extracts
        .Where(pr => !string.IsNullOrWhiteSpace(pr.Section))
        .Where(pr => !Regex.IsMatch(pr.Section, @"^Page ?\d+$"))
        .Select(s => new Section(book, s.Section, s.RowIndex, _settings.CellsPerPage))
        .OrderBy(s => s.SourceRowIndex);

      // Clock the sections one by one into a HashSet. 
      // In the event that duplicate section names exist,
      // only the first instance (by name) will be kept:
      foreach (var section in sections) {
        sectionsSet.Add(section);
      }

      return sectionsSet.ToList();
    }
  }
}