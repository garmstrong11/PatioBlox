namespace PatioBlox2016.DataAccess.Config
{
	using System.Data.Entity.ModelConfiguration;
	using Concrete;

	public class BookTypeConfiguration : EntityTypeConfiguration<Book>
	{
		public BookTypeConfiguration()
		{
			HasRequired(b => b.JobFile)
				.WithRequiredDependent()
				.WillCascadeOnDelete(false);
		} 
	}
}