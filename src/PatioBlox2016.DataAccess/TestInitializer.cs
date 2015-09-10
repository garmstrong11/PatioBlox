namespace PatioBlox2016.DataAccess
{
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.IO;

  public class TestInitializer : DropCreateDatabaseAlways<PatioBloxContext>
  {
    protected override void Seed(PatioBloxContext context)
    {
      var keywordsFile = Path.Combine(GetSeedPath(), "Keywords.sql");
      //var descriptionsFile = Path.Combine(GetSeedPath(), "Descriptions.sql");

      context.Database.ExecuteSqlCommand(File.ReadAllText(keywordsFile));
      //context.Database.ExecuteSqlCommand(File.ReadAllText(descriptionsFile));
      var descriptions = SeedHelpers.GetDescriptionSeeds();
      foreach (var description in descriptions) {
        context.Descriptions.AddOrUpdate(d => d.Text, description);
      }

      SeedHelpers.SeedKeywords(context);

      context.SaveChanges();

      base.Seed(context);
    }
  }
}