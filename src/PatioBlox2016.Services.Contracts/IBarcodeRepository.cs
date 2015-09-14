namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IBarcodeRepository : IRepository<Barcode>
  {
    Dictionary<string, Barcode> GetBarcodeDictionary();

    IEnumerable<string> FilterExisting(IEnumerable<string> upcs);
  }
}