namespace PatioBlox2016.DataAccess.Migrations
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity.Migrations;
  using System.IO.Abstractions;
  using System.Linq;
  using Concrete;
  using Extractor;

  internal sealed class Configuration : DbMigrationsConfiguration<PatioBloxContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(PatioBloxContext context)
    {
      AddDataFromExcelSeedFile(context);
    }

    private static string SeedPath
    {
      get
      {
        var nameDict = new Dictionary<string, string>
                       {
                         {"PRANK", @"C:\Users\gma\Dropbox\PatioBlox\Seed.xlsx"},
                         {"GARMSTRONG", @"C:\Users\garmstrong\Documents\My Dropbox\PatioBlox\Seed.xlsx"},
                         {"WS01IT-GARMSTRO", @"C:\Users\garmstrong\Dropbox\PatioBlox\Seed.xlsx"}
                       };
        var machine = Environment.MachineName;

        string path;
        if (nameDict.TryGetValue(machine, out path)) return path;
        
        throw new InvalidOperationException("Seed.xlsx can't be found. Check your Dropbox path.");
      }
    }

    private static void AddDataFromExcelSeedFile(PatioBloxContext context)
    {
      var dapter = new FlexCelDataSourceAdapter();
      var fileSystem = new FileSystem();

      var extractor = new SeedExtractor(dapter, fileSystem);
      extractor.Initialize(SeedPath);
      var agg = extractor.Extract().FirstOrDefault();

      if (agg == null) throw new InvalidOperationException("Seed file extraction failed");

      foreach (var kw in agg.KeywordDtos.Select(dto => new Keyword(dto))) {
        context.Keywords.AddOrUpdate(p => p.Word, kw);
      }

      foreach (var job in agg.JobDtos.Select(dto => new Job(dto))) {
        context.Jobs.AddOrUpdate(j => j.PrinergyJobId, job);
      }

      context.SaveChanges();
      var jobs = context.Jobs.ToList();

      context.SaveChanges();
      var keyWords = context.Keywords.ToList();

      foreach (var dto in agg.ExpansionDtos)
      {
        var kw = keyWords.FirstOrDefault(k => k.Word == dto.Expansion);
        if (kw == null) continue;

        var expansion = new Expansion(dto) { KeywordId = kw.Id };
        context.Expansions.AddOrUpdate(e => e.Word, expansion);
      }

      context.SaveChanges();
    }
  }
}
