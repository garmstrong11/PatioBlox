namespace PatioBlox2016.Tests.ConcreteTests
{
	using System;
	using Concrete;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class KeywordDtoTests
	{
		[Test]
		public void Constructor_WordTypeArgumentDoesNotParse_Throws()
		{
			// ReSharper disable once ObjectCreationAsStatement
			Action act = () => new KeywordDto("ASHBERRY", 6, "NotExist");

			act.ShouldThrow<ArgumentException>().WithMessage("*line 6*");
		}

		[Test]
		public void Constructor_WordArgumentEmpty_Throws()
		{
			// ReSharper disable once ObjectCreationAsStatement
			Action act = () => new KeywordDto("", 6, "Color");

			act.ShouldThrow<ArgumentNullException>().WithMessage("*Parameter name: word");
		}
	}
}