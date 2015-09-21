namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  public class Section
  {
    //private readonly List<Cell> _cells;
    private readonly List<Page> _pages; 
    
    public Section(Book book, string name, int rowIndex)
    {
      if (book == null) throw new ArgumentNullException("book");
      if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");

      Book = book;
      SectionName = name;
      SourceRowIndex = rowIndex - 1;

      _pages = new List<Page>();

      //_cells = new List<Cell>();
    }

    public string SectionName { get; private set; }
    public int SourceRowIndex { get; private set; }

    public Book Book { get; private set; }

    public IReadOnlyList<Page> Pages
    {
      get { return new ReadOnlyCollection<Page>(_pages);}
    }

    public void AddPage(Page page)
    {
      _pages.Add(page);
    }

    public void AddPageRange(IEnumerable<Page> pages)
    {
      _pages.AddRange(pages);
    }

    public int PageCount
    {
      get
      {
        var count = _pages.Count;
        var mod = count % 2;
        return mod == 0 ? count : count + 1;
      }
    }

    public override string ToString()
    {
      return SectionName;
    }
  }
}