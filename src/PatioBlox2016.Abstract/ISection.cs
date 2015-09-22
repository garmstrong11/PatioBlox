namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  public interface ISection
  {
    string SectionName { get; }
    int SourceRowIndex { get; }
    IBook Book { get; }
    ReadOnlyCollection<ICell> Cells { get; }
    IReadOnlyList<IPage> Pages { get; }
    int PageCount { get; }

    void AddCell(ICell cell);
    void AddCellRange(IEnumerable<ICell> cells);
    void RemoveCell(ICell cell);
  }
}