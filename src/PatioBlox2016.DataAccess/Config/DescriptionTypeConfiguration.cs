namespace PatioBlox2016.DataAccess.Config
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Infrastructure.Annotations;
  using System.Data.Entity.ModelConfiguration;
  using Concrete;

  public class DescriptionTypeConfiguration : EntityTypeConfiguration<Description>
  {
    public DescriptionTypeConfiguration()
    {
      Property(p => p.Text)
        .HasMaxLength(255)
        .IsRequired()
        .HasColumnAnnotation(
          IndexAnnotation.AnnotationName,
          new IndexAnnotation(new IndexAttribute {IsUnique = true}));

      Property(p => p.Vendor).HasMaxLength(50);
      Property(p => p.Color).HasMaxLength(50);
      Property(p => p.Size).HasMaxLength(50);
      Property(p => p.Name).HasMaxLength(50);
    } 
  }
}