namespace PatioBlox2016.Services.EfImpl
{
	using Concrete;
	using PatioBlox2016.DataAccess;

  public class BookRepository : RepositoryBase<Book>
	{
		public BookRepository(PatioBloxContext context) : base(context) {}
	}
}