namespace PatioBlox.Tests.DomainTests
{
	using System.Collections.Generic;
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class PatchTests
	{
		private readonly PatioBlock _blok1 = new PatioBlock 
			{ ItemNumber = 1, Barcode = "ABC", PalletQuantity = "12", Description = "Desc" };

		private readonly PatioBlock _blok2 = new PatioBlock 
			{ ItemNumber = 2, Barcode = "DEF", PalletQuantity = "12", Description = "Desc" };

		private readonly PatioBlock _blok3 = new PatioBlock 
			{ ItemNumber = 3, Barcode = "GHI", PalletQuantity = "12", Description = "Desc" };
		
		[Test]
		public void SectionsEqual_returns_true_when_PatioBlocks_match_in_content_and_order()
		{
			var patch1 = new Patch
				{
				Name = "AB",
				Sections = new List<Section>
					{
					new Section {Name = "Rock", PatioBlocks = new List<PatioBlock> {_blok1, _blok2}},
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
					}
				};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
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
					new Section {Name = "Rock", PatioBlocks = new List<PatioBlock> {_blok2, _blok1}},
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
					}
			};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
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
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
					new Section {Name = "Rock", PatioBlocks = new List<PatioBlock> {_blok2, _blok1}},
					}
			};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
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
					new Section {Name = "Rock", PatioBlocks = new List<PatioBlock> {_blok1, _blok2}},
					}
			};

			var patch2 = new Patch
			{
				Name = "AC",
				Sections = new List<Section>
					{
					new Section {Name = "Paper", PatioBlocks = new List<PatioBlock> {_blok1, _blok2}},
					new Section {Name = "Scissors", PatioBlocks = new List<PatioBlock> {_blok2, _blok3}},
					}
			};

			patch1.SectionsEqual(patch2).Should().BeFalse();
		}
	}
}