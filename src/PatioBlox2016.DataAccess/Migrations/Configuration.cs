namespace PatioBlox2016.DataAccess.Migrations
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity.Migrations;
  using System.IO;

  internal sealed class Configuration : DbMigrationsConfiguration<PatioBloxContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(PatioBloxContext context)
    {
      var keywordsFile = Path.Combine(GetSeedPath(), "Keywords.sql");
      var expansionsFile = Path.Combine(GetSeedPath(), "Expansions.sql");
      var descriptionsFile = Path.Combine(GetSeedPath(), "Descriptions.sql");

      context.Database.ExecuteSqlCommand(File.ReadAllText(keywordsFile));
      context.Database.ExecuteSqlCommand(File.ReadAllText(expansionsFile));
      context.Database.ExecuteSqlCommand(File.ReadAllText(descriptionsFile));

      base.Seed(context);
    }

    private static string GetSeedPath()
    {
      var nameDict = new Dictionary<string, string>
                      {
                        {"PRANK", @"C:\Users\gma\Dropbox\PatioBlox\TestSeed"},
                        {"GARMSTRONG", @"C:\Users\garmstrong\Documents\My Dropbox\PatioBlox\TestSeed"},
                        {"WS01IT-GARMSTRO", @"C:\Users\garmstrong\Dropbox\PatioBlox\TestSeed"}
                      };
      var machine = Environment.MachineName;

      string path;
      if (nameDict.TryGetValue(machine, out path)) return path;

      throw new InvalidOperationException("Seed path can't be found. Check your Dropbox path.");
    }
  }
}
