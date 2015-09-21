namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Abstract;
  using Concrete;

  public class BookFactory : IBookFactory
  {
    private readonly ICellFactory _cellFactory;
    private readonly ISettingsService _settings;

    public BookFactory(ICellFactory cellFactory, ISettingsService settings)
    {
      _cellFactory = cellFactory;
      _settings = settings;
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

      // Filter patchRows with Sku = -1
      var cells = extracts
        .Where(e => e.Sku >= 0)
        .Select(pr => _cellFactory.CreateCell(pr));

      // May throw CellConstructionException:
      foreach (var cell in cells) {
        cell.FindSection(sections);
      }

      // Filter empty sections and add to book:
      book.AddSectionRange(sections.Where(s => s.Cells.Any()));

      return book;
    }

    private List<Section> ExtractSections(Book book, IEnumerable<IPatchRowExtract> extracts)
    {
      var sections = extracts
        .Where(pr => !string.IsNullOrWhiteSpace(pr.Section))
        .Where(pr => !Regex.IsMatch(pr.Section, @"^Page ?\d+$"))
        .Select(s => new Section(book, s.Section, s.RowIndex, _settings.CellsPerPage));

      return sections.Distinct(new SectionNameEqualityComparer()).ToList();
    }
  }
}