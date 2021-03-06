﻿namespace PatioBlox2016.Tests.ConcreteTests
{
  using Concrete;
  using FluentAssertions;
  using NUnit.Framework;

  [TestFixture]
  public class KeywordTests
  {
    private Keyword _root;
    private Keyword _expansion;
    private Keyword _abbreviation;

    [SetUp]
    public void RunBeforeEachTest()
    {
      _root = new Keyword("COLOR");
      _expansion = new Keyword("ASPEN") { Parent = _root };
      _abbreviation = new Keyword("ASPN") { Parent = _expansion };
    }

    [Test]
    public void WordTypeGet_AbbreviatedMember_FindsWordType()
    {
      _abbreviation.RootWord.Should().Be(Keyword.ColorKey);
    }

    [Test]
    public void WordTypeGet_UnknownRoot_FindsWordTypeAsName()
    {
      var newRoot = new Keyword("Unknown");
      var expan = new Keyword("ASPEN") {Parent = newRoot};
      expan.RootWord.Should().Be(Keyword.NameKey);
    }

    [Test]
    public void WordTypeGet_ExpandedMember_FindsWordType()
    {
      _expansion.RootWord.Should().Be(Keyword.ColorKey);
    }

    [Test]
    public void WordTypeGet_RootMember_FindsWordType()
    {
      _root.RootWord.Should().Be(Keyword.ColorKey);
    }

    [Test]
    public void PropertyExpansion_ExpandedMember_ReturnsSelfWord()
    {
      _expansion.Expansion.Should().Be(_expansion.Word);
    }

    [Test]
    public void PropertyExpansion_AbbreviatedMember_ReturnsParent()
    {
      _abbreviation.Expansion.Should().Be(_expansion.Word);
    }

    [Test]
    public void PropertyExpansion_RootMember_ReturnsSelfWord()
    {
      _root.Expansion.Should().Be(_root.Word);
    }
  }
}