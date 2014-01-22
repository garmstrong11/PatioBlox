namespace PatioBlox.Tests.DomainTests
{
	using System.Collections.Generic;
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class PatchTests
	{
		private readonly Product _blok1 = new Product 
			{ ItemNumber = 1, Barcode = new Barcode("ABC"), PalletQuantity = "12", Description = "Desc" };

		private readonly Product _blok2 = new Product 
			{ ItemNumber = 2, Barcode = new Barcode("DEF"), PalletQuantity = "12", Description = "Desc" };

		private readonly Product _blok3 = new Product 
			{ ItemNumber = 3, Barcode = new Barcode("GHI"), PalletQuantity = "12", Description = "Desc" };
		
		[Test]
		public void SectionsEqual_returns_true_when_PatioBlocks_match_in_content_and_order()
		{
			var patch1 = new Patch
				{
				Name = "AB",
				Sections = new List<Section>
					{
					new Section {Name = "Rock", PatioBlocks = new List<Product> {_blok1, _blok2}},
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok2, _blok3}},
					}
				};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<Product> {_blok2, _blok3}},
					}
			};

			patch1.SectionsEqual(patch2).Should().BeTrue();
		}

		[Test]
		public void SectionsEqual_returns_false_when_PatioBlocks_dont_match_in_content_and_order()
		{
			var patch1 = new Patch
			{
				Name = "AB",
				Sections = new List<Section>
					{
					new Section {Name = "Rock", PatioBlocks = new List<Product> {_blok2, _blok1}},
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok2, _blok3}},
					}
			};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<Product> {_blok2, _blok3}},
					}
			};

			patch1.SectionsEqual(patch2).Should().BeFalse();
		}

		[Test]
		public void SectionsEqual_returns_false_when_PatioBlocks_dont_match_in_order()
		{
			var patch1 = new Patch
			{
				Name = "AB",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok2, _blok3}},
					new Section {Name = "Rock", PatioBlocks = new List<Product> {_blok2, _blok1}},
					}
			};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<Product> {_blok2, _blok3}},
					}
			};

			patch1.SectionsEqual(patch2).Should().BeFalse();
		}

		[Test]
		public void SectionsEqual_returns_false_when_section_counts_dont_match()
		{
			var patch1 = new Patch
			{
				Name = "AB",
				Sections = new List<Section>
					{
					new Section {Name = "Rock", PatioBlocks = new List<Product> {_blok1, _blok2}},
					}
			};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<Product> {_blok2, _blok3}},
					}
			};

			patch1.SectionsEqual(patch2).Should().BeFalse();
		}

		[Test]
		public void ToMetrixString_returns_correct_metrix_csv_string()
		{
			var patch = new Patch
				{
				Name = "AC",
				StoreCount = 2,
				Id = 0,
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<Product> {_blok1, _blok2, _blok3, _blok1, _blok1}},
					new Section {Name = "Scissors", PatioBlocks = new List<Product> {_blok2, _blok3, _blok2, _blok2, _blok2}},
					}
				};

			var expected = "AC_01,1,24,8.5,5.5,,,AC_01.pdf,,,,,,,,\n";
			expected += "AC_02,1,24,8.5,5.5,,,AC_02.pdf,,,,,,,,\n";
			expected += "AC_03,1,24,8.5,5.5,,,AC_03.pdf,,,,,,,,\n";
			expected += "AC_04,1,24,8.5,5.5,,,AC_04.pdf,,,,,,,,\n";

			var actual = patch.ToMetrixString();

			actual.Should().Be(expected);
		}
	}
}