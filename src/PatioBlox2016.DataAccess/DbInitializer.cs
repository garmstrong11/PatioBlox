namespace PatioBlox2016.DataAccess
{
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.IO.Abstractions;
	using System.Linq;
	using Concrete;
	using Extractor;

	class DbInitializer : DropCreateDatabaseIfModelChanges<PatioBloxContext>
	{
		protected override void Seed(PatioBloxContext context)
		{
			var dapter = new FlexCelDataSourceAdapter();
			var fileSystem = new FileSystem();
			const string seedPath = @"C:\Users\garmstrong\Documents\My Dropbox\PatioBlox\SeedData.xlsx";

			var extractor = new SeedExtractor(dapter, fileSystem);
			extractor.Initialize(seedPath);
			var agg = extractor.Extract().FirstOrDefault();

			//agg.KeywordDtos.ForEach(k => context.Keywords.AddOrUpdate(new Keyword(k)));

			foreach (var dto in agg.KeywordDtos) {
				var kw = new Keyword(dto);
				context.Keywords.AddOrUpdate(kw);
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
