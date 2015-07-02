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
		public void DatabaseFileIsCreated()
		{
			var ctx = new PatioBloxContext();
			var wordList = ctx.Keywords.ToList();

			File.Exists(@"F:\Lowes\PatioBlocks2016\PatioBloxDb.sdf").Should().BeTrue();
			var expan = ctx.Expansions.First(e => e.KeywordId == 5);

			expan.Keyword.WordType.Should().Be(WordType.Vendor);
		}
	}
}