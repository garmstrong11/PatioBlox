namespace PatioBlox2016.Concrete
{
  using Abstract;

  public class UpcReplacement : IUpcReplacement
  {
    public UpcReplacement()
    {
      // Set a default value for newly created objects
      Replacement = "111111111111";
    }

    public UpcReplacement(string invalidUpc)
    {
      InvalidUpc = invalidUpc;
      Replacement = invalidUpc;
    }
    
    public int Id { get; set; }
    public string InvalidUpc { get; set; }
    public string Replacement { get; set; }
  }
}