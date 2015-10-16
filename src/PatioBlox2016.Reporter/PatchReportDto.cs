namespace PatioBlox2016.Reporter
{
  using Abstract;

  public class PatchReportDto : IPatchReportDto
  {
    public PatchReportDto(string name, int pageCount, int storeCount, int copiesPerStore)
    {
      Name = name;
      PageCount = pageCount;
      StoreCount = storeCount;
      CopiesPerStore = copiesPerStore;
    }
    
    public string Name { get; private set; }
    public int PageCount { get; private set; }
    public int StoreCount { get; private set; }
    public int CopiesPerStore { get; private set; }
  }
}