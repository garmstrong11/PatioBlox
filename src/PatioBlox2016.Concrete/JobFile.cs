namespace PatioBlox2016.Concrete
{
  using Seeding;

  public class JobFile
  {
    /// <summary>
    /// A protected, parameterless constructor for Entity Framework.
    /// </summary>
    protected JobFile()
    {}
    
    public JobFile(JobFileDto dto)
    {
      Id = 0;
      FileName = dto.FileName;
    }

    public int Id { get; private set; }
    public int JobId { get; set; }
    public string FileName { get; set; }
  }
}