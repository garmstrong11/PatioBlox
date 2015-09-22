namespace PatioBlox2016.Services.EfImpl
{
  using System.Collections.Generic;
  using System.Linq;
  using Concrete;
  using DataAccess;
  using Contracts;

  public class UpcReplacementRepository :RepositoryBase<UpcReplacement>, IUpcReplacementRepository
  {
    public UpcReplacementRepository(PatioBloxContext context) : base(context) {}
    public IDictionary<string, string> GetUpcReplacementDictionary()
    {
      return GetAll().ToDictionary(k => k.InvalidUpc, v => v.Replacement);
    }
  }
}