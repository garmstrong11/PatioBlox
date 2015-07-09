namespace PatioBlox2016.DataAccess
{
	using System.Data.Entity;
	using Concrete;
	using Config;

	public class PatioBloxContext : DbContext
	{
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Expansion> Expansions { get; set; }
		public DbSet<Description> Descriptions { get; set; }
		public DbSet<DescriptionUsage> DescriptionUsages { get; set; }

		public DbSet<Job> Jobs { get; set; }
		// A Job has JobFiles:
		public DbSet<JobFile> JobFiles { get; set; }
		// A Job has Books:
		public DbSet<Book> Books { get; set; }
		// A Book has Sections:
		public DbSet<Section> Sections { get; set; }
		// A Section has Pages:
		public DbSet<Page> Pages { get; set; }
		// A Page has Cells:
		public DbSet<Cell> Cells { get; set; }

		public PatioBloxContext() : base("name=PatioBloxConnectionString")
	  {
      Database.SetInitializer(
				new MigrateDatabaseToLatestVersion<PatioBloxContext, Migrations.Configuration>());
	  }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new KeywordTypeConfiguration());
			modelBuilder.Configurations.Add(new ExpansionTypeConfiguration());
		  modelBuilder.Configurations.Add(new JobTypeConfiguration());
		  modelBuilder.Configurations.Add(new JobFileTypeConfiguration());
			//modelBuilder.Configurations.Add(new BookTypeConfiguration());
		}
	}
}