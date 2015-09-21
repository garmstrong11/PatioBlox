namespace PatioBlox2016.Abstract
{
  public interface ISection
  {
    int Id { get; set; }
    SectionName SectionName { get; set; }
    int SectionNameId { get; set; }
    int SourceRowIndex { get; set; }
    Book Book { get; set; }
    int BookId { get; set; }
    ICollection<Cell> Cells { get; set; }
    int GetPageCount(int cellsPerPage);
  }
}