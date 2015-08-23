namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;

  public class Section
  {
    public int Id { get; set; }
    
    public Section()
    {
      Cells = new List<Cell>();
    }
    
    public Section(Book book, SectionName sectionName) : this()
    {
      if (sectionName == null) throw new ArgumentNullException("sectionName");

      Book = book;
      SectionName = sectionName;
    }

    public SectionName SectionName { get; set; }
    public int SectionNameId { get; set; }
    public int SourceRowIndex { get; set; }

    public Book Book { get; set; }
    public int BookId { get; set; }

    public ICollection<Cell> Cells { get; set; }

    public int GetPageCount(int cellsPerPage)
    {
      var count = Cells.Count / cellsPerPage;
      var mod = Cells.Count % cellsPerPage;
      return mod == 0 ? count : count + 1;
    }
  }
}