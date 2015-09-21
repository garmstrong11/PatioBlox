namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IKeywordRepository : IRepository<Keyword>
  {
    Dictionary<string, Keyword> GetKeywordDictionary();
    IEnumerable<string> FilterExisting(IEnumerable<string> words);
  }
}