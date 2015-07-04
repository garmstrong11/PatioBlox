namespace PatioBlox2016.Tests.DataAccessTests
{
	using System.IO;
	using System.Linq;
	using Concrete;
	using DataAccess;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class DBTests
	{
		[Test]
		public void DatabaseIsCreated()
		{
			var ctx = new PatioBloxContext();
			var expan = ctx.Expansions.First(e => e.KeywordId == 5);

			expan.Keyword.WordType.Should().Be(WordType.Color);
		}
	}
}