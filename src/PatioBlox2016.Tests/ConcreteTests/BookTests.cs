namespace PatioBlox2016.Tests.ConcreteTests
{
  using System;
  using System.Collections.Generic;
  using FakeItEasy;
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;

  [TestFixture]
  public class BookTests
  {
    private ISection _sec1;
    private ISection _sec2;

    [SetUp]
    public void RunBeforeEachTest()
    {
      _sec1 = A.Fake<ISection>();
      _sec2 = A.Fake<ISection>();
      A.CallTo(() => _sec1.PageCount).Returns(5);
      A.CallTo(() => _sec2.PageCount).Returns(4);
    }

    [Test]
    public void Ctor_NullJob_Throws()
    {
      Action act = () => new Book(null, "AB");

      act.ShouldThrow<ArgumentNullException>().WithMessage("*job*");
    }

    [Test]
    public void Ctor_EmptyName_Throws()
    {
      var job = A.Fake<IJob>();
      Action act = () => new Book(job, "  ");

      act.ShouldThrow<ArgumentNullException>().WithMessage("*bookName*");
    }
    
    [Test]
    public void PropertyGet_PageCount_ReturnsEvenNumberWithOddPageTotal()
    {
      var job = A.Fake<IJob>();
      var book = new Book(job, "VW");
      book.AddSection(_sec1);
      book.AddSection(_sec2);

      book.PageCount.Should().Be(10);
    }
    
    [Test]
    public void PropertyGet_PdfFileNames_Correct()
    {
      var job = A.Fake<IJob>();
      var book = new Book(job, "VW");
      book.AddSection(_sec1);
      book.AddSection(_sec2);

      var expected = new List<string>
      {
        "VW_01-02.pdf",
        "VW_03-04.pdf",
        "VW_05-06.pdf",
        "VW_07-08.pdf",
        "VW_09-10.pdf"
      };

      var actual = book.PdfFileNames;
      actual.Should().Equal(expected);
    }
  }
}