namespace PatioBlox2016.DataAccess
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.IO;
  using Concrete;

  public class TestInitializer : DropCreateDatabaseAlways<PatioBloxContext>
  {
    protected override void Seed(PatioBloxContext context)
    {
      //var keywordsFile = Path.Combine(GetSeedPath(), "Keywords.sql");
      var descriptionsFile = Path.Combine(GetSeedPath(), "Descriptions.sql");

      //context.Database.ExecuteSqlCommand(File.ReadAllText(keywordsFile));
      context.Database.ExecuteSqlCommand(File.ReadAllText(descriptionsFile));
      context.Keywords.AddRange(MakeKeywordSeeds());

      context.SaveChanges();

      base.Seed(context);
      base.Seed(context);
    }

    private static IEnumerable<Keyword> MakeKeywordSeeds()
    {
      var adobe = new Keyword("ADOBE") { WordType = WordType.Color };
      var allegheny = new Keyword("ALLEGHENY") { WordType = WordType.Color };
      var alghn = new Keyword("ALGHN") { WordType = WordType.Color, Expansion = allegheny };
      var alghny = new Keyword("ALGHNY") { WordType = WordType.Color, Expansion = allegheny };
      var algny = new Keyword("ALGNY") { WordType = WordType.Color, Expansion = allegheny };
      var allghny = new Keyword("ALLGHNY") { WordType = WordType.Color, Expansion = allegheny };

      var bevl = new Keyword("BEVELED") { WordType = WordType.Name};
      var pacc   = new Keyword("PACIFICCLAY") { WordType = WordType.Vendor};
      var ricc = new Keyword("RICCOBENE") { WordType = WordType.Vendor};
      var capu = new Keyword("CAPPUCCINO") {WordType = WordType.Color};
      var marq = new Keyword("MARQUETTE") {WordType = WordType.Name};
      var surr = new Keyword("SURREY") {WordType = WordType.Color};
      var toff = new Keyword("TOFFEE") {WordType = WordType.Color};
      var chap = new Keyword("CHAPARRAL") {WordType = WordType.Color};
      var cnst = new Keyword("COUNYRTSTONE") {WordType = WordType.Vendor};
      var ctsd = new Keyword("COUNTRYSIDE") {WordType = WordType.Name};
      var cssy = new Keyword("CASSAY") {WordType = WordType.Vendor};
      var four = new Keyword("FOURCOBBLE") {WordType = WordType.Name};
      var fred = new Keyword("FREDERICK") {WordType = WordType.Name};
      var terr = new Keyword("TERRACOTTA") {WordType = WordType.Color};
      var wtrd = new Keyword("WEATHERED") {WordType = WordType.Name};

      return new List<Keyword>()
             {
               adobe, allegheny, alghn, alghny, algny, allghny, bevl, pacc,
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