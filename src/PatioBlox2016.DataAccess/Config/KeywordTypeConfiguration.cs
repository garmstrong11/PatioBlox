namespace PatioBlox2016.DataAccess.Config
{
	using System.Data.Entity.ModelConfiguration;
	using Concrete;

	public class KeywordTypeConfiguration : EntityTypeConfiguration<Keyword>
	{
		public KeywordTypeConfiguration()
		{
			Property(d => d.Word).IsRequired().HasMaxLength(25);
			Property(d => d.WordType).IsRequired();
		}
	}
}