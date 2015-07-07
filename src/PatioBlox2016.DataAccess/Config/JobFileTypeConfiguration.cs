namespace PatioBlox2016.DataAccess.Config
{
  using System.Data.Entity.ModelConfiguration;
  using Concrete;

  public class JobFileTypeConfiguration : EntityTypeConfiguration<JobFile>
  {
    public JobFileTypeConfiguration()
    {
      Property(p => p.FileName).HasMaxLength(255);
    }
  }
}