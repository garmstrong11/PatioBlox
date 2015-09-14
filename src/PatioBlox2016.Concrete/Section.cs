namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;

  public class Section
  {
    private readonly List<Cell> _cells; 
    public Section()
    {
      _cells = new List<Cell>();
    }
    
    public Section(Book book) : this()
    {
      if (book == null) throw new ArgumentNullException("book");

      Book = book;
    }

    public string SectionName { get; set; }
    public int SourceRowIndex { get; set; }

    public Book Book { get; set; }

    public void AddCell(Cell cell)
    {
      _cells.Add(cell);
    }

    public List<Cell> Cells
    {
      get { return new List<Cell>(_cells); }
    } 

    public IEnumerable<Page> GetPages(int cellsPerPage)
    {
      List<Cell> bucket = null;
      var count = 0;

      foreach (var cell in _cells) {
        if (bucket == null) {
          bucket = new List<Cell>(cellsPerPage);
        }

        bucket[count++] = cell;

        // The bucket is fully populated before it's yielded
        if (count != cellsPerPage) {
          continue;
        }

        yield return new Page(this, bucket);

        bucket = null;
        count = 0;
      }

      // Yield the remaining cells 
      if (bucket != null && count > 0) {
        yield return new Page(this, bucket);
      }
    } 

    public int GetPageCount(int cellsPerPage)
    {
      var count = _cells.Count / cellsPerPage;
      var mod = _cells.Count % cellsPerPage;
      return mod == 0 ? count : count + 1;
    }
  }
}