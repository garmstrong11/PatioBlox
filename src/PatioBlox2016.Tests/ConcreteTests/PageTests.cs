namespace PatioBlox2016.Tests.ConcreteTests
{
  using System.Collections.Generic;
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Concrete;

  [TestFixture]
  public class PageTests
  {
    [Test]
    public void ToJsxString_ReturnsCorrect()
    {
      var cell1 = new Cell(69, 11111, "128", "Descr")
      {
        Color = "Color1",
        Upc = "Upc1",
        Size = "Size1",
        Name = "Name1"
      };

      var cell2 = new Cell(70, 11112, "129", "Descr2")
      {
        Color = "Color2",
        Upc = "Upc2",
        Size = "Size2",
        Name = "Name2"
      };

      var page = new Page(new Section(new Book(new Job(), "AB"), "Section Name", 19, 4), new List<Cell> {cell1, cell2});
      var actual = page.ToJsxString(3);

      actual.Should().Be("foo");
    }
  }
}