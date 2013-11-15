namespace PatioBlox.Tests.DomainTests
{
	using System.Collections.Generic;
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class SectionTests
	{
		private readonly PatioBlock _blok1 = new PatioBlock
			{ItemNumber = 1, Barcode = "ABC", PalletQuantity = "12", Description = "Desc"};

		private readonly PatioBlock _blok2 = new PatioBlock
			{ItemNumber = 2, Barcode = "DEF", PalletQuantity = "12", Description = "Desc"};

		private readonly PatioBlock _blok3 = new PatioBlock
			{ItemNumber = 3, Barcode = "GHI", PalletQuantity = "12", Description = "Desc"};
		
		[Test]
		public void SectionEquals_returns_true_when_sequence_members_are_identical_in_Content_and_order()
		{
			var blox1 = new List<PatioBlock> {_blok1, _blok2, _blok3};
			var blox2 = new List<PatioBlock> {_blok1, _blok2, _blok3};

			var sec1 = new Section {Id = 1, Index = 1, Name = "sec1", PatioBlocks = blox1};
			var sec2 = new Section { Id = 2, Index = 2, Name = "sec2", PatioBlocks = blox2 };

			sec1.PatioBlocksEqual(sec2).Should().BeTrue();
		}

		[Test]
		public void SectionEquals_returns_false_when_sequence_members_are_not_identical_in_order()
		{
			var blox1 = new List<PatioBlock> { _blok1, _blok2, _blok3 };
			var blox2 = new List<PatioBlock> { _blok1, _blok3, _blok2 };

			var sec1 = new Section { Id = 1, Index = 1, Name = "sec1", PatioBlocks = blox1 };
			var sec2 = new Section { Id = 2, Index = 2, Name = "sec2", PatioBlocks = blox2 };

			sec1.PatioBlocksEqual(sec2).Should().BeFalse();
		}

		[Test]
		public void SectionEquals_returns_false_when_one_is_empty()
		{
			var blox1 = new List<PatioBlock> { _blok1, _blok2, _blok3 };
			var blox2 = new List<PatioBlock> ();

			var sec1 = new Section { Id = 1, Index = 1, Name = "sec1", PatioBlocks = blox1 };
			var sec2 = new Section { Id = 2, Index = 2, Name = "sec2", PatioBlocks = blox2 };

			sec1.PatioBlocksEqual(sec2).Should().BeFalse();
		}
	}
}