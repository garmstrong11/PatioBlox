namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IPage : IJsxExportable
  {
    ISection Section { get; }
    IReadOnlyList<ICell> Cells { get; }
    string Header { get; }
    void AddCellRange(IEnumerable<ICell> cells);
    void AddCell(ICell cell);
  }
}