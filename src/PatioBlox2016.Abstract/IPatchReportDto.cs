namespace PatioBlox2016.Abstract
{
  public interface IPatchReportDto
  {
    string Name { get; }
    int PageCount { get; }
    int StoreCount { get; }
    int CopiesPerStore { get; }
  }
}