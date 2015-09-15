namespace PatioBlox2016.DataAccess
{
	using System.Data.Entity;
	using Concrete;
	using Config;

	public class PatioBloxContext : DbContext
	{
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Description> Descriptions { get; set; }
    public DbSet<Barcode> Barcodes { get; set; } 
    public DbSet<BarcodeCorrection> BarcodeCorrections { get; set; } 


		public PatioBloxContext() : base("name=PatioBloxConnectionString")
		{
		  Database.SetInitializer(
        //new TestInitializer());
        new NullDatabaseInitializer<PatioBloxContext>());
		    //new DropCreateDatabaseAlways<PatioBloxContext>());
		    //new MigrateDatabaseToLatestVersion<PatioBloxContext, Migrations.Configuration>());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
      modelBuilder.Configurations.Add(new KeywordTypeConfiguration());
		  modelBuilder.Configurations.Add(new DescriptionTypeConfiguration());
		  modelBuilder.Configurations.Add(new BarcodeTypeConfiguration());
		}
	}
}