namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IUpcReplacementRepository : IRepository<UpcReplacement>
  {
    IDictionary<string, string> GetUpcReplacementDictionary(); 
  }
}