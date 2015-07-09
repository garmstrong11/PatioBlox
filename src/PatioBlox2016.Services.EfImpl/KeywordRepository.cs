namespace PatioBlox2016.Services.EfImpl
{
	using System.Data.Entity;
	using Concrete;

	public class KeywordRepository : RepositoryBase<Keyword>
	{
		public KeywordRepository(DbContext context) : base(context) {}
	}
}