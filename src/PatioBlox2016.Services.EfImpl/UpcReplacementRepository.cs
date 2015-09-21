namespace PatioBlox2016.Services.EfImpl
{
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.DataAccess;
  using PatioBlox2016.Services.Contracts;

  public class UpcReplacementRepository :RepositoryBase<UpcReplacement>, IUpcReplacementRepository
  {
    public UpcReplacementRepository(PatioBloxContext context) : base(context) {}
    public IDictionary<string, string> GetUpcReplacementDictionary()
    {
      return GetAll().ToDictionary(k => k.InvalidUpc, v => v.Replacement);
    }
  }
}