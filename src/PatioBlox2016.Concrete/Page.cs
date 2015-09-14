namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;

  public class Page
  {
    public Page(Section section, List<Cell> cells)
    {
      Section = section;
      Cells = cells;
    }
    
    public Section Section { get; private set; }
    public List<Cell> Cells { get; private set; }
  }
}