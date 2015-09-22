namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;
  using Abstract;

  public class Section : ISection
  {
    private readonly int _cellsPerPage;
    private readonly List<ICell> _cells;
    
    public Section(IBook book, string name, int rowIndex, int cellsPerPage)
    {
      _cellsPerPage = cellsPerPage;
      if (book == null) throw new ArgumentNullException("book");
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

      Book = book;
      SectionName = name;
      SourceRowIndex = rowIndex - 1;

      _cells = new List<ICell>();
    }

    public string SectionName { get; private set; }
    public int SourceRowIndex { get; private set; }

    public IBook Book { get; private set; }

    public void AddCell(ICell cell)
    {
      _cells.Add(cell);
    }

    public void AddCellRange(IEnumerable<ICell> cells)
    {
      _cells.AddRange(cells);
    }

    public void RemoveCell(ICell cell)
    {
      _cells.Remove(cell);
    }

    public ReadOnlyCollection<ICell> Cells
    {
      get { return _cells.AsReadOnly(); }
    }

    public IReadOnlyList<IPage> Pages
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

    private IEnumerable<IEnumerable<ICell>> SplitCellsIntoPages()
    {
      List<ICell> bucket = null;
      var count = 0;

      foreach (var cell in _cells)
      {
        if (bucket == null)
        {
          bucket = new List<ICell>();
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