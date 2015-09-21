namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;

  public class Section
  {
    private readonly int _cellsPerPage;
    private readonly List<Cell> _cells;
    
    public Section(Book book, string name, int rowIndex, int cellsPerPage)
    {
      _cellsPerPage = cellsPerPage;
      if (book == null) throw new ArgumentNullException("book");
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

      Book = book;
      SectionName = name;
      SourceRowIndex = rowIndex - 1;

      _cells = new List<Cell>();
    }

    public string SectionName { get; private set; }
    public int SourceRowIndex { get; private set; }

    public Book Book { get; private set; }

    public void AddCell(Cell cell)
    {
      _cells.Add(cell);
    }

    public void AddCellRange(IEnumerable<Cell> cells)
    {
      _cells.AddRange(cells);
    }

    public void RemoveCell(Cell cell)
    {
      _cells.Remove(cell);
    }

    public ReadOnlyCollection<Cell> Cells
    {
      get { return _cells.AsReadOnly(); }
    }

    public IReadOnlyList<Page> Pages
    {
      get
      {
        var cellGroups = SplitCellsIntoPages();

        return cellGroups
          .Select(cg => new Page(this, cg.OrderBy(c => c.SourceRowIndex)))
          .ToList();
      }
    }

    public int PageCount
    {
      get { return Pages.Count; }
    }

    public override string ToString()
    {
      return SectionName;
    }

    private IEnumerable<IEnumerable<Cell>> SplitCellsIntoPages()
    {
      List<Cell> bucket = null;
      var count = 0;

      foreach (var cell in _cells)
      {
        if (bucket == null)
        {
          bucket = new List<Cell>();
        }

        bucket.Add(cell);
        count++;

        // The bucket is fully populated before it's yielded
        if (count != _cellsPerPage)
        {
          continue;
        }

        yield return bucket.AsEnumerable();

        bucket = null;
        count = 0;
      }

      // Yield the remaining cells 
      if (bucket != null && count > 0)
      {
        yield return bucket.AsEnumerable();
      }
    }
  }
}