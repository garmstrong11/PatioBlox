namespace PatioBlox2016.DataAccess
{
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.IO.Abstractions;
	using System.Linq;
	using Concrete;
	using Extractor;

	class DbInitializer : CreateDatabaseIfNotExists<PatioBloxContext>
	{
		protected override void Seed(PatioBloxContext context)
		{
			var dapter = new FlexCelDataSourceAdapter();
			var fileSystem = new FileSystem();
			//const string seedPath = @"C:\Users\garmstrong\Documents\My Dropbox\PatioBlox\SeedData.xlsx";
      const string seedPath = @"C:\Users\gma\Dropbox\PatioBlox\Seed.xlsx";

			var extractor = new SeedExtractor(dapter, fileSystem);
			extractor.Initialize(seedPath);
			var agg = extractor.Extract().FirstOrDefault();

		  var dtoKeywords = agg.KeywordDtos.Select(dto => new Keyword(dto));
		  context.Keywords.AddRange(dtoKeywords);

		  var dtoJobs = agg.JobDtos.Select(dto => new Job(dto));
		  context.Jobs.AddRange(dtoJobs);

		  context.SaveChanges();
		  var jobs = context.Jobs.ToList();

		  foreach (var dto in agg.JobFileDtos) {
		    var job = jobs.FirstOrDefault(j => j.PrinergyJobId == dto.PrinergyJobId);
        if (job == null) continue;

		    var jobFile = new JobFile(dto) {JobId = job.Id};
		    context.JobFiles.AddOrUpdate(jobFile);
		  }

			context.SaveChanges();
			var keyWords = context.Keywords.ToList();

			foreach (var dto in agg.ExpansionDtos) {
				var kw = keyWords.FirstOrDefault(k => k.Word == dto.Expansion);
				if (kw == null) continue;

				var expansion = new Expansion(dto) {KeywordId = kw.Id};
				context.Expansions.AddOrUpdate(expansion);
			}

			context.SaveChanges();
		}
	}
}
