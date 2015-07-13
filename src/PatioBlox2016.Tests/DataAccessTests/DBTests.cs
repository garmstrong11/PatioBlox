namespace PatioBlox2016.Tests.DataAccessTests
{
  using System;
  using System.Data;
  using System.Linq;
	using Concrete;
	using DataAccess;
	using FluentAssertions;
	using NUnit.Framework;
  using System.Data.Entity;
  using System.Data.Entity.Infrastructure;
  using ExtractorTests;

	[TestFixture]
	public class DbTests : ExtractorTestBase
	{
	  private PatioBloxContext _ctx;
    
    [TestFixtureSetUp]
	  public void Init()
	  {
	    _ctx = new PatioBloxContext();
	  }
    
    [Test]
		public void DatabaseIsCreated()
		{
			var expan = _ctx.Expansions.Include(k => k.Keyword).First(e => e.KeywordId == 5);

			expan.Keyword.WordType.Should().Be(WordType.Color);
		}

		[Test]
		public void CanInsertJob()
		{
			var job = new Job(167125, 2016, @"\\Storage2\AraxiVolume_SAN\Jobs\Lowes US 2016 Patio Blocks");
			//var jobFile1 = new JobFile(job, Patch1Path);
			//job.JobFiles.Add(jobFile1);
			var book1 = new Book(job, "AB");
			job.Books.Add(book1);
			
			_ctx.Jobs.Add(job);

			_ctx.SaveChanges();
		}

	  [Test]
	  public void DuplicateDescriptionTextIsNotAllowed()
	  {
	    const string text = "BOOGA BOOGA";
      var desc1 = new Description(69, text);
	    _ctx.Descriptions.Add(desc1);

      var desc2 = new Description(70, text);
	    _ctx.Descriptions.Add(desc2);

	    Action act = () => _ctx.SaveChanges();
	    act.ShouldThrow<DbUpdateException>();
	  }
	}
}