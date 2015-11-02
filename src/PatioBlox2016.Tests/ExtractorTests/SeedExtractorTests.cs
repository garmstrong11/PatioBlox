namespace PatioBlox2016.Tests.ExtractorTests
{
	using System.IO.Abstractions;
	using System.Linq;
	using Concrete;
	using Extractor;
	using FakeItEasy;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class SeedExtractorTests
	{
		private string seedPath;
		private SeedAggregate _seedAggregate;
		
		[TestFixtureSetUp]
		public void Init()
		{
			seedPath = @"\\Storage2\AraxiVolume_1183xx\Factory\Lowes\PatioBlox\dev\SeedData.xlsx";
			var dapter = new FlexCelDataSourceAdapter();
			var fileSystem = new FileSystem();

			var seedExtractor = new SeedExtractor(dapter, fileSystem);
			seedExtractor.Initialize(seedPath);

			_seedAggregate = seedExtractor.Extract().FirstOrDefault();
		}

		[Test]
		public void CanExtractSeedData()
		{
			_seedAggregate.Should().NotBeNull();
		}

		[Test]
		public void Extract_DuplicateKeywordsAreRemoved()
		{
			// Seed file has a duplicate "ASH" value.
			var ashCount = _seedAggregate.KeywordDtos.Count(c => c.Word == "ASH");

			// Check to make sure only one "ASH" entry is present:
			ashCount.Should().Be(1);
		}
	}
}