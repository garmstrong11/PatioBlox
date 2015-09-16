namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;

  public interface IUpcReplacementRepository : IRepository<UpcReplacement>
  {
    IDictionary<string, string> GetUpcReplacementDictionary();
  }
}