namespace PatioBlox2016.Concrete
{
  public class Barcode
  {
    private Barcode() { }

    public Barcode(string upc) : this()
    {
      Id = 0;
      Upc = upc;
    }
    
    public int Id { get; private set; }
    public string Upc { get; set; }
  }
}