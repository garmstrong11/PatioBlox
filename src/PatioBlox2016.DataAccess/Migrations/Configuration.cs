namespace PatioBlox2016.DataAccess.Migrations
{
  using System;
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
      //  This method will be called after migrating to the latest version.

      //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
      //  to avoid creating duplicate seed data. E.g.
      //
      //    context.People.AddOrUpdate(
      //      p => p.FullName,
      //      new Person { FullName = "Andrew Peters" },
      //      new Person { FullName = "Brice Lambson" },
      //      new Person { FullName = "Rowan Miller" }
      //    );
      //
      AddDataFromExcelSeedFile(context);
    }

    private void AddDataFromExcelSeedFile(PatioBloxContext context)
    {
      var dapter = new FlexCelDataSourceAdapter();
      var fileSystem = new FileSystem();
      const string seedPath = @"C:\Users\garmstrong\Documents\My Dropbox\PatioBlox\Seed.xlsx";
      //const string seedPath = @"C:\Users\gma\Dropbox\PatioBlox\Seed.xlsx";

      var extractor = new SeedExtractor(dapter, fileSystem);
      extractor.Initialize(seedPath);
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

      foreach (var dto in agg.JobFileDtos)
      {
        var job = jobs.FirstOrDefault(j => j.PrinergyJobId == dto.PrinergyJobId);
        if (job == null) continue;

        var jobFile = new JobFile(dto) { JobId = job.Id };
        context.JobFiles.AddOrUpdate(f => f.FileName, jobFile);
      }

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
