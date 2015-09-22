namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IProduct
  {
    int Sku { get; }
    string Upc { get; set; }
    List<IUsageLocation> Usages { get; }
    bool IsBarcodeInvalid { get; }
    List<string> BarcodeErrors { get; }
    bool HasPatchProductDuplicates { get; }
  }
}