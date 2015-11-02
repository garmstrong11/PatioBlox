namespace PatioBlox2016.DataAccess
{
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Linq;
  using Concrete;
	using Config;

	public class PatioBloxContext : DbContext
	{
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Description> Descriptions { get; set; }
    public DbSet<UpcReplacement> UpcReplacements { get; set; }
    public DbSet<JobSource> JobSources { get; set; }

		public PatioBloxContext() : base("name=PatioBloxConnectionString")
		{
		  Database.SetInitializer(
      new TestInitializer());
		  //new FullInitializer());
		  //new NullDatabaseInitializer<PatioBloxContext>());
		  //new DropCreateDatabaseAlways<PatioBloxContext>());
		  //new MigrateDatabaseToLatestVersion<PatioBloxContext, Migrations.Configuration>());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
      modelBuilder.Configurations.Add(new KeywordTypeConfiguration());
		  modelBuilder.Configurations.Add(new DescriptionTypeConfiguration());
		  modelBuilder.Configurations.Add(new UpcReplacementTypeConfiguration());
		  modelBuilder.Configurations.Add(new JobSourceTypeConfiguration());
		}

	  public Dictionary<string, int> DescriptionDict
	  {
	    get { return Descriptions.Local.ToDictionary(k => k.Text, v => v.Id); }
	  } 
	}
}