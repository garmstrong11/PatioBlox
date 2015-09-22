namespace PatioBlox2016.Tests.ConcreteTests
{
  using FluentAssertions;
  using NUnit.Framework;
  using Concrete;

  [TestFixture]
  public class CellTests
  {
    [Test]
    public void ToJsxString_ReturnsExpected()
    {
      var cell = new Cell(69, 11111, "128", "Descr")
      {
        DescriptionId = 420,
        Upc = "Upc"
      };

      var jsx = cell.ToJsxString(4);

      jsx
        .Should()
        .Be("\t\t\t\t{ 'sku' : 11111, 'upc' : 'Upc', 'descriptionId' : 420, 'palletQty' : '128' }");
    }
  }
}