namespace PatioBlox2018.Impl.AbstractReporter
{
  public interface IPatchReportDto
  {
    string Name { get; }
    int PageCount { get; }
    int StoreCount { get; }
    int CopiesPerStore { get; }
  }
}