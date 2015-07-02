namespace PatioBlox2016.DataAccess.Config
{
	using System.Data.Entity.ModelConfiguration;
	using Concrete;

	public class ExpansionTypeConfiguration : EntityTypeConfiguration<Expansion>
	{
		public ExpansionTypeConfiguration()
		{
			Property(p => p.KeywordId).IsRequired();
			Property(p => p.Word).IsRequired().HasMaxLength(25);
		}
	}
}