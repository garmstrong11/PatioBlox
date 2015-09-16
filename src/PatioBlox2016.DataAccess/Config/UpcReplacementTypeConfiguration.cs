namespace PatioBlox2016.DataAccess.Config
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Infrastructure.Annotations;
  using System.Data.Entity.ModelConfiguration;
  using PatioBlox2016.Concrete;

  public class UpcReplacementTypeConfiguration : EntityTypeConfiguration<UpcReplacement>
  {
    public UpcReplacementTypeConfiguration()
    {
      Property(p => p.Replacement).HasMaxLength(25);
      
      Property(d => d.InvalidUpc)
        .IsRequired()
        .HasMaxLength(25)
        .HasColumnAnnotation(
          IndexAnnotation.AnnotationName,
          new IndexAnnotation(new IndexAttribute { IsUnique = true }));
    }
  }
}