namespace PatioBlox2016.Services.EfImpl
{
  using System.Collections.Generic;
  using System.Linq;
  using Contracts;
  using Concrete;
  using DataAccess;

  public class KeywordRepository : RepositoryBase<Keyword>, IKeywordRepository
	{
		public KeywordRepository(PatioBloxContext context) : base(context) {}

    public Dictionary<string, Keyword> GetKeywordDictionary()
    {
      return GetAll().ToDictionary(k => k.Word);
    }
	}
}