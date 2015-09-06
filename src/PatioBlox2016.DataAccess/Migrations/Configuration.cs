namespace PatioBlox2016.DataAccess.Migrations
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity.Migrations;
  using System.IO;
  using Concrete;

  internal sealed class Configuration : DbMigrationsConfiguration<PatioBloxContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(PatioBloxContext context)
    {
      //var keywordsFile = Path.Combine(GetSeedPath(), "Keywords.sql");
      var descriptionsFile = Path.Combine(GetSeedPath(), "Descriptions.sql");

      //context.Database.ExecuteSqlCommand(File.ReadAllText(keywordsFile));
      context.Database.ExecuteSqlCommand(File.ReadAllText(descriptionsFile));
      context.Keywords.AddRange(MakeKeywordSeeds());

      context.SaveChanges();

      base.Seed(context);
    }

    private static IEnumerable<Keyword> MakeKeywordSeeds()
    {
      var adobe = new Keyword("ADOBE") {WordType = WordType.Color};
      var allegheny = new Keyword("ALLEGHENY") {WordType = WordType.Color};
      var alghn = new Keyword("ALGHN") {WordType = WordType.Color, Expansion = allegheny};
      var alghny = new Keyword("ALGHNY") {WordType = WordType.Color, Expansion = allegheny};
      var algny = new Keyword("ALGNY") {WordType = WordType.Color, Expansion = allegheny};
      var allghny = new Keyword("ALLGHNY") {WordType = WordType.Color, Expansion = allegheny};

      return new List<Keyword>() {adobe, allegheny, alghn, alghny, algny, allghny};
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
