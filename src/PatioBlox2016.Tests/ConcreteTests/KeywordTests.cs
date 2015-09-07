namespace PatioBlox2016.Tests.ConcreteTests
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
    public void PropertyRoot_AbbreviatedMember_FindsRoot()
    {
      _abbreviation.Root.Should().Be(_root.Word);
    }

    [Test]
    public void PropertyRoot_ExpandedMember_FindsRoot()
    {
      _expansion.Root.Should().Be(_root.Word);
    }

    [Test]
    public void PropertyRoot_RootMember_FindsRoot()
    {
      _root.Root.Should().Be(_root.Word);
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