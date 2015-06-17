namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IProduct
  {
    int Sku { get; }
    string Color { get; set; }
    string Size { get; set; }
    string Name { get; set; }
    List<string> Descriptions { get; } 
  }
}