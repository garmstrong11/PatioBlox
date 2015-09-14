namespace PatioBlox2016.Services.EfImpl
{
  using System.Collections.Generic;
  using System.Linq;
  using Concrete;
  using Contracts;
  using DataAccess;

  public class BarcodeRepository : RepositoryBase<Barcode>, IBarcodeRepository
  {
    public BarcodeRepository(PatioBloxContext context) : base(context)
    {
    }

    public Dictionary<string, Barcode> GetBarcodeDictionary()
    {
      return GetAll().ToDictionary(k => k.Upc);
    }

    public IEnumerable<string> FilterExisting(IEnumerable<string> upcs)
    {
      return upcs.Except(GetAll().Select(b => b.Upc));
    }
  }
}