namespace PatioBlox2016.Reporter
{
  using Abstract;

  public class PatchReportDto : IPatchReportDto
  {
    public PatchReportDto(string name, int pageCount, int storeCount)
    {
      Name = name;
      PageCount = pageCount;
      StoreCount = storeCount;
    }
    
    public string Name { get; private set; }
    public int PageCount { get; private set; }
    public int StoreCount { get; private set; }
  }
}