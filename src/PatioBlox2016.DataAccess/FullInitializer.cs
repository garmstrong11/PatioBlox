namespace PatioBlox2016.DataAccess
{
  using System.Data.Entity;

  public class FullInitializer : DropCreateDatabaseAlways<PatioBloxContext>
  {
    protected override void Seed(PatioBloxContext context)
    {
      SeedHelpers.FullSeed(context);

      base.Seed(context);
    }
  }
}