namespace PatioBlox2016.DataAccess.Config
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Infrastructure.Annotations;
  using System.Data.Entity.ModelConfiguration;
  using Concrete;

  public class SectionNameTypeConfiguration : EntityTypeConfiguration<SectionName>
  {
    public SectionNameTypeConfiguration()
    {
      Property(p => p.Value)
        .IsRequired()
        .HasMaxLength(25)
        .HasColumnAnnotation(
          IndexAnnotation.AnnotationName,
          new IndexAnnotation(new IndexAttribute {IsUnique = true})
        );
    }
  }
}