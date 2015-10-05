namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface ICell : IJsxExportable
  {
    int SourceRowIndex { get; }
    int Sku { get; }
    string Description { get; }
    string PalletQty { get; }
    int DescriptionId { get; set; }
    string Upc { get; set; }
    ISection Section { get; set; }
    IPage Page { get; set; }
    string Image { get; }
    void FindSection(IEnumerable<ISection> sections);
  }
}