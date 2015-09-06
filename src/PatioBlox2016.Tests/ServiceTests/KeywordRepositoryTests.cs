namespace PatioBlox2016.Tests.ServiceTests
{
	using System.Linq;
	using Concrete;
	using Concrete.Seeding;
	using DataAccess;
	using FluentAssertions;
	using NUnit.Framework;
	using Services.EfImpl;

	[TestFixture]
	public class KeywordRepositoryTests
	{
		private KeywordRepository _repo;
		
		[TestFixtureSetUp]
		public void Init()
		{
			var context = new PatioBloxContext();
			_repo = new KeywordRepository(context);
		}

		[Test]
		public void CanGetAllKeywords()
		{
			var keywords = _repo.GetAll();

			keywords.Should().NotBeEmpty();
		}

		[Test]
		public void CanFindKeyword()
		{
			var adobe = _repo.Find(k => k.Word == "ADOBE");

			adobe.Should().NotBeNull();
		}

    //[Test]
    //public void CanAddKeyword()
    //{
    //  var dto = new KeywordDto("HASSLE", 69, "Name");
    //  var kw = new Keyword(dto);

    //  _repo.Add(kw);

    //  var storedKw = _repo.Find(k => k.Word == "HASSLE");
    //  storedKw.Any().Should().BeTrue();
    //}
	}
}