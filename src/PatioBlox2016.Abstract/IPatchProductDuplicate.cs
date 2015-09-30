namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IPatchProductDuplicate
  {
    int Sku { get; }
    string Upc { get; }
    string PatchName { get; set; }
    IReadOnlyList<int> RowIndices { get; }

    void AddIndex(int index);
    void AddIndexRange(IEnumerable<int> indices);
  }
}