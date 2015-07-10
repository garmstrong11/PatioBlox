namespace PatioBlox2016.Services.EfImpl
{
	using System.Data.Entity;
	using Concrete;

	public class JobFileRepository : RepositoryBase<JobFile>
	{
		public JobFileRepository(DbContext context) : base(context) {}
	}
}