namespace PatioBlox2016.DataAccess.Migrations
{
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<PatioBloxContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    //protected override void Seed(PatioBloxContext context)
    //{
    //  var descriptions = SeedHelpers.GetDescriptionSeeds();
    //  foreach (var description in descriptions)
    //  {
    //    context.Descriptions.AddOrUpdate(d => d.Text, description);
    //  }

    //  SeedHelpers.SeedKeywords(context);
    //  context.SaveChanges();

    //  base.Seed(context);
    //}
  }
}
