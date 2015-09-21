namespace PatioBlox2016.Tests.ConcreteTests
{
  using FluentAssertions;
  using NUnit.Framework;
  using PatioBlox2016.Concrete;

  [TestFixture]
  public class CellTests
  {
    [Test]
    public void ToJsxString_ReturnsExpected()
    {
      var cell = new Cell(69, 11111, "128", "Descr")
      {
        Color = "Color",
        Upc = "Upc",
        Size = "Size",
        Name = "Name"
      };

      var jsx = cell.ToJsxString(4);

      jsx
        .Should()
        .Be("\t\t\t\t{ 'sku' : 11111, 'upc' : 'Upc', 'size' : 'Size', 'color' : 'Color', 'name' : 'Name', 'palletQty' : '128' }");
    }
  }
}