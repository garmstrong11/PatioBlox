namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IPage
  {
    IEnumerable<ICell> Cells { get; }
    ISection Section { get; }

    void AddCell(ICell cell);
    int CellCount { get; }
  }
}