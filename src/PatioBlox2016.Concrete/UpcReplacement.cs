namespace PatioBlox2016.Concrete
{
  public class UpcReplacement
  {
    public UpcReplacement()
    {
      // Set a default value for newly created objects
      Replacement = "111111111111";
    }
    
    public int Id { get; set; }
    public string InvalidUpc { get; set; }
    public string Replacement { get; set; }
  }
}