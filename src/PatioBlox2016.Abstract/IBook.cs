namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IBook
  {
    int Id { get; }
    Job Job { get; set; }
    int JobId { get; set; }
    string BookName { get; }
    HashSet<Section> Sections { get; set; }
    bool HasDuplicateCells { get; }
    IEnumerable<IEnumerable<int>> DuplicateCellGroups { get; }
    IEnumerable<string> DuplicateCellReports { get; }
    int GetPageCount(int cellsPerPage);
  }
}