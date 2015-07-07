namespace PatioBlox2016.DataAccess
{
	using System.Data.Entity;
	using Concrete;
	using Config;

	public class PatioBloxContext : DbContext
	{
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Expansion> Expansions { get; set; }
	  public DbSet<Job> Jobs { get; set; }
	  public DbSet<JobFile> JobFiles { get; set; }

	  public PatioBloxContext() : base("name=PatioBloxConnectionString")
	  {
      Database.SetInitializer(new CreateDatabaseIfNotExists<PatioBloxContext>());
	  }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new KeywordTypeConfiguration());
			modelBuilder.Configurations.Add(new ExpansionTypeConfiguration());
		  modelBuilder.Configurations.Add(new JobTypeConfiguration());
		  modelBuilder.Configurations.Add(new JobFileTypeConfiguration());
		}
	}
}