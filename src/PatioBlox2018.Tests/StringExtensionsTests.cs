namespace PatioBlox2018.Tests
{
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2018.Impl;
  using LanguageExt;
  using static LanguageExt.Prelude;

  [TestFixture]
  public class StringExtensionsTests
  {
    [Test]
    public void GetLastDigit_NullString_ReturnsNone()
    {
      const string subject = null;

      var check = subject.GetLastDigit() == None;
      check.Should().BeTrue();
    }
  }
}