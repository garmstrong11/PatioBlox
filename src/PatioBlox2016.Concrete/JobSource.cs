namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;

  public class JobSource
  {
    public JobSource()
    {
      Descriptions = new List<Description>();
    }
    
    public int Id { get; set; }
    public ICollection<Description>  Descriptions { get; set; }
    public string JobPath { get; set; }
  }
}