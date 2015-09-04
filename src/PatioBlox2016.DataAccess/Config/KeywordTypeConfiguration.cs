namespace PatioBlox2016.DataAccess.Config
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Infrastructure.Annotations;
  using System.Data.Entity.ModelConfiguration;
	using Concrete;

	internal class KeywordTypeConfiguration : EntityTypeConfiguration<Keyword>
	{
		public KeywordTypeConfiguration()
		{
		  HasKey(k => k.Id);

      Property(d => d.Word)
        .IsRequired()
        .HasMaxLength(25)
        .HasColumnAnnotation(
          IndexAnnotation.AnnotationName,
          new IndexAnnotation(new IndexAttribute {IsUnique = true}));

			Property(d => d.WordType).IsRequired();
		}
	}
}