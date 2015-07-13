namespace PatioBlox2016.DataAccess.Config
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Infrastructure.Annotations;
  using System.Data.Entity.ModelConfiguration;
  using Concrete;

  public class BarcodeTypeConfiguration : EntityTypeConfiguration<Barcode>
  {
    public BarcodeTypeConfiguration()
    {
      Property(p => p.Upc)
        .IsRequired()
        .HasMaxLength(20)
        .HasColumnAnnotation(
          IndexAnnotation.AnnotationName,
          new IndexAnnotation(new IndexAttribute {IsUnique = true})
        );
    }
  }
}