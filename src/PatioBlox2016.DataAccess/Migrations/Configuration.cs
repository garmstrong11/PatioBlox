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
      var color = new Keyword("COLOR");
      var vendor = new Keyword("VENDOR");
      var name = new Keyword("NAME");
      var adobe = new Keyword("ADOBE") { Parent = color };
      var allegheny = new Keyword("ALLEGHENY") { Parent = color };
      var alghn = new Keyword("ALGHN") { Parent = allegheny };
      var alghny = new Keyword("ALGHNY") { Parent = allegheny };
      var algny = new Keyword("ALGNY") { Parent = allegheny };
      var allghny = new Keyword("ALLGHNY") { Parent = allegheny };

      var bevl = new Keyword("BEVELED") { Parent = name };
      var pacc = new Keyword("PACIFICCLAY") { Parent = vendor };
      var ricc = new Keyword("RICCOBENE") { Parent = vendor };
      var capu = new Keyword("CAPPUCCINO") { Parent = color };
      var marq = new Keyword("MARQUETTE") { Parent = name };
      var surr = new Keyword("SURREY") { Parent = color };
      var toff = new Keyword("TOFFEE") { Parent = color };
      var chap = new Keyword("CHAPARRAL") { Parent = color };
      var cnst = new Keyword("COUNTRYSTONE") { Parent = vendor };
      var ctsd = new Keyword("COUNTRYSIDE") { Parent = name };
      var cssy = new Keyword("CASSAY") { Parent = vendor };
      var four = new Keyword("FOURCOBBLE") { Parent = name };
      var fred = new Keyword("FREDERICK") { Parent = name };
      var terr = new Keyword("TERRACOTTA") { Parent = color };
      var wtrd = new Keyword("WEATHERED") { Parent = name };

      return new List<Keyword>()
             {
               name, color, vendor, adobe, allegheny, alghn, alghny, algny, allghny, bevl, pacc,
               ricc, capu, marq, surr, toff, chap, cnst, ctsd, cssy, four,
               fred, terr, wtrd
             };
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
