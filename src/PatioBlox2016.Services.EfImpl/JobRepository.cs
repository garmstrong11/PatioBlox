namespace PatioBlox2016.Services.EfImpl
{
	using System.Data.Entity;
	using Concrete;

	public class JobRepository : RepositoryBase<Job>
	{
		public JobRepository(DbContext context) : base(context) {}
	}
}