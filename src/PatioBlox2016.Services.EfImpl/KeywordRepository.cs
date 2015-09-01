namespace PatioBlox2016.Services.EfImpl
{
  using PatioBlox2016.Concrete;
  using PatioBlox2016.DataAccess;

  public class KeywordRepository : RepositoryBase<Keyword>
	{
		public KeywordRepository(PatioBloxContext context) : base(context) {}
	}
}