namespace PatioBlox2016.Tests.DataAccessTests
{
	using System.Linq;
	using Concrete;
	using DataAccess;
	using FluentAssertions;
	using NUnit.Framework;
  using System.Data.Entity;

	[TestFixture]
	public class DbTests
	{
		[Test]
		public void DatabaseIsCreated()
		{
			var ctx = new PatioBloxContext();
			var expan = ctx.Expansions.Include(k => k.Keyword).First(e => e.KeywordId == 5);

			expan.Keyword.WordType.Should().Be(WordType.Color);
		}
	}
}