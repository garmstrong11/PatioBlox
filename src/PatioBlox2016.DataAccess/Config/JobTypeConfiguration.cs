namespace PatioBlox2016.DataAccess.Config
{
  using System.Data.Entity.ModelConfiguration;
  using Concrete;

  public class JobTypeConfiguration : EntityTypeConfiguration<Job>
  {
    public JobTypeConfiguration()
    {
      Property(p => p.Path).HasMaxLength(255);
    }
  }
}