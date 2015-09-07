namespace PatioBlox2016.DataAccess
{
	using System.Data.Entity;
	using Concrete;
	using Config;

	public class PatioBloxContext : DbContext
	{
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Description> Descriptions { get; set; }

		public DbSet<Job> Jobs { get; set; }
		public DbSet<Book> Books { get; set; }
	  public DbSet<Section> Sections { get; set; }
	  public DbSet<SectionName> SectionNames { get; set; }
		public DbSet<Cell> Cells { get; set; }

		public PatioBloxContext() : base("name=PatioBloxConnectionString")
		{
		  Database.SetInitializer(
        new TestInitializer());
        //new NullDatabaseInitializer<PatioBloxContext>());
		    //new DropCreateDatabaseAlways<PatioBloxContext>());
		    //new MigrateDatabaseToLatestVersion<PatioBloxContext, Migrations.Configuration>());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
      modelBuilder.Configurations.Add(new KeywordTypeConfiguration());
		  modelBuilder.Configurations.Add(new JobTypeConfiguration());
		  modelBuilder.Configurations.Add(new DescriptionTypeConfiguration());
		  modelBuilder.Configurations.Add(new BarcodeTypeConfiguration());
		  modelBuilder.Configurations.Add(new BookTypeConfiguration());
		}
	}
}