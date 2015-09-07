namespace PatioBlox2016.DataAccess
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.IO;
  using System.Linq;
  using Concrete;

  public class TestInitializer : DropCreateDatabaseAlways<PatioBloxContext>
  {
    private readonly Keyword _color;
    private readonly Keyword _name;
    private readonly Keyword _vendor;

    public TestInitializer()
    {
      var rootSeeds = Enum.GetNames(typeof(WordType))
        .Select(w => new Keyword(w.ToUpper()))
        .ToList();

      _color = rootSeeds.Single(k => k.Word == "COLOR");
      _vendor = rootSeeds.Single(k => k.Word == "VENDOR");
      _name = rootSeeds.Single(k => k.Word == "NAME");
    }

    protected override void Seed(PatioBloxContext context)
    {
      var keywordsFile = Path.Combine(GetSeedPath(), "Keywords.sql");
      var descriptionsFile = Path.Combine(GetSeedPath(), "Descriptions.sql");

      // Turn off the foreign key constrainst during loading...
      context.Database.ExecuteSqlCommand("ALTER TABLE Keywords NOCHECK CONSTRAINT ALL");
      context.Database.ExecuteSqlCommand(File.ReadAllText(keywordsFile));
      // Turn the foreign key constraints back on after loading...
      context.Database.ExecuteSqlCommand("ALTER TABLE Keywords CHECK CONSTRAINT ALL");

      context.Database.ExecuteSqlCommand(File.ReadAllText(descriptionsFile));

      //context.Keywords.Add(_name);
      //context.SaveChanges();
      //context.Keywords.Add(_color);
      //context.SaveChanges();
      //context.Keywords.Add(_vendor);
      //context.SaveChanges();
      //context.Keywords.AddRange(MakeKeywordSeeds());

      context.SaveChanges();

      base.Seed(context);
    }

    private IEnumerable<Keyword> MakeKeywordSeeds()
    {
      var newSeeds = new List<Keyword>();

      newSeeds.AddRange(new List<Keyword> {
                       new Keyword("ALLEGHENY") {Parent = _color},
                       new Keyword("BEVELED") {Parent = _name},
                       new Keyword("PACIFICCLAY") {Parent = _vendor},
                       new Keyword("RICCOBENE") {Parent = _vendor},
                       new Keyword("CAPPUCCINO") {Parent = _color},
                       new Keyword("MARQUETTE") {Parent = _name},
                       new Keyword("SURREY") {Parent = _color},
                       new Keyword("TOFFEE") {Parent = _color},
                       new Keyword("CHAPARRAL") {Parent = _color},
                       new Keyword("COUNTRYSTONE") {Parent = _vendor},
                       new Keyword("COUNTRYSIDE") {Parent = _name},
                       new Keyword("CASSAY") {Parent = _vendor},
                       new Keyword("FOURCOBBLE") {Parent = _name},
                       new Keyword("FREDERICK") {Parent = _name},
                       new Keyword("TERRACOTTA") {Parent = _color},
                       new Keyword("WEATHERED") {Parent = _name},
                       new Keyword("SIDE") {Parent = _name}
                     });

      return newSeeds;
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