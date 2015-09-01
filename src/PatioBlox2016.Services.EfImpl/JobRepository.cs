namespace PatioBlox2016.Services.EfImpl
{
	using Concrete;
	using PatioBlox2016.DataAccess;

  public class JobRepository : RepositoryBase<Job>
	{
		public JobRepository(PatioBloxContext context) : base(context) {}
	}
}