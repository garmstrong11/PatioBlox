namespace PatioBlox2016.DataAccess.Config
{
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Data.Entity.Infrastructure.Annotations;
  using System.Data.Entity.ModelConfiguration;
  using PatioBlox2016.Concrete;

  public class JobSourceTypeConfiguration : EntityTypeConfiguration<JobSource>
  {
    public JobSourceTypeConfiguration()
    {
      Property(p => p.JobPath)
        .HasMaxLength(255)
        .IsRequired()
        .HasColumnAnnotation(
          IndexAnnotation.AnnotationName,
          new IndexAnnotation(new IndexAttribute {IsUnique = true}));
    }
  }
}