namespace PatioBlox2016.DataAccess
{
	using System.Data.Entity;
	using Concrete;
	using Config;

	public class PatioBloxContext : DbContext
	{
		public DbSet<Keyword> Keywords { get; set; }
		public DbSet<Expansion> Expansions { get; set; }

		static PatioBloxContext()
		{
			Database.SetInitializer(new DbInitializer());

			using (var db = new PatioBloxContext()) db.Database.Initialize(false);
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new KeywordTypeConfiguration());
			modelBuilder.Configurations.Add(new ExpansionTypeConfiguration());
		}
	}
}