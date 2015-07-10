namespace PatioBlox2016.Tests.DataAccessTests
{
	using System.Linq;
	using Concrete;
	using DataAccess;
	using FluentAssertions;
	using NUnit.Framework;
  using System.Data.Entity;
	using ExtractorTests;

	[TestFixture]
	public class DbTests : ExtractorTestBase
	{
		[Test]
		public void DatabaseIsCreated()
		{
			var ctx = new PatioBloxContext();
			var expan = ctx.Expansions.Include(k => k.Keyword).First(e => e.KeywordId == 5);

			expan.Keyword.WordType.Should().Be(WordType.Color);
		}

		[Test]
		public void CanInsertJob()
		{
			var ctx = new PatioBloxContext();

			var job = new Job(167125, 2016, @"\\Storage2\AraxiVolume_SAN\Jobs\Lowes US 2016 Patio Blocks");
			var jobFile1 = new JobFile(job, Patch1Path);
			job.JobFiles.Add(jobFile1);
			var book1 = new Book(jobFile1, "AB");
			jobFile1.Books.Add(book1);
			
			ctx.Jobs.Add(job);

			ctx.SaveChanges();
		}
	}
}