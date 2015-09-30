namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2016.Abstract;

  public class PatchProductDuplicate : IPatchProductDuplicate
  {
    private readonly List<int> _rowIndices;

    public PatchProductDuplicate()
    {
      _rowIndices = new List<int>();
    }

    public PatchProductDuplicate(int sku, string upc) : this()
    {
      Sku = sku;
      Upc = upc;
    }

    public int Sku { get; private set; }
    public string Upc { get; private set; }
    public string PatchName { get; set; }

    public IReadOnlyList<int> RowIndices
    {
      get { return _rowIndices
        .OrderBy(n => n)
        .ToList()
        .AsReadOnly(); }
    }

    public void AddIndex(int index)
    {
      _rowIndices.Add(index);
    }

    public void AddIndexRange(IEnumerable<int> indices)
    {
      _rowIndices.AddRange(indices);
    }

    public override string ToString()
    {
      var indices = string.Join(" and ", RowIndices);
      return string.Format("Patch {2} has a duplicate item {0} (upc {1}) in rows {3}", Sku, Upc, PatchName, indices);
    }
  }
}