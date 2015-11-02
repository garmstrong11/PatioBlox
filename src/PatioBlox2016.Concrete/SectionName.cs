namespace PatioBlox2016.Concrete
{
  public class SectionName
  {
    private SectionName() { }

    public SectionName(string value) : this()
    {
      Id = 0;
      Value = value;
    }
    
    public int Id { get; private set; }
    public string Value { get; set; }
  }
}