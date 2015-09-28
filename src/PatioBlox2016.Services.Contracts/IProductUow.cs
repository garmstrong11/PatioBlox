namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using Abstract;

  public interface IProductUow
  {
    IEnumerable<IProduct> GetProducts();
  }
}