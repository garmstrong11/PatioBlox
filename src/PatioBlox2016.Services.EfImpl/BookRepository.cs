namespace PatioBlox2016.Services.EfImpl
{
	using System.Data.Entity;
	using Concrete;

	public class BookRepository : RepositoryBase<Book>
	{
		public BookRepository(DbContext context) : base(context) {}
	}
}