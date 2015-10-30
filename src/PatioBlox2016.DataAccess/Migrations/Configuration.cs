namespace PatioBlox2016.DataAccess.Migrations
{
  using System.Data.Entity.Migrations;

  public sealed class Configuration : DbMigrationsConfiguration<PatioBloxContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(PatioBloxContext context)
    {
      SeedHelpers.TestSeed(context);
      //SeedHelpers.FullSeed(context);

      base.Seed(context);
    }
  }
}
