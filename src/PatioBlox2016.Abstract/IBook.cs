namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IBook : IJsxExportable
  {
    IJob Job { get; set; }
    string BookName { get; }
    IReadOnlyCollection<ISection> Sections { get; }
    bool HasDuplicateCells { get; }

    IEnumerable<IEnumerable<int>> DuplicateCellGroups { get; }
    IEnumerable<string> DuplicateCellReports { get; }
    int PageCount { get; }

    void AddSection(ISection section);
    void AddSectionRange(IEnumerable<ISection> sections);
    void RemoveSection(ISection section);

    IEnumerable<string> PdfFileNames { get; }

    void SetPageIndices();
  }
}