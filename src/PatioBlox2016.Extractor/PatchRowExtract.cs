namespace PatioBlox2016.Extractor
{
  using PatioBlox2018.Core;

  /// <summary>
  /// A simple data transfer object to encapsulate rows
  /// extracted from an Excel patch content worksheet.
  /// </summary>
  public class PatchRowExtract : IPatchRowExtract
  {
    public PatchRowExtract(
      string patchName, int sourceRowIndex, string section, 
      int? blockIndex, int? sku, string vendor, 
      string description, string palletQuanity, string barcode)
    {
      PatchName = patchName;
      SourceRowIndex = sourceRowIndex;
      Section = section;
      BlockIndex = blockIndex;
      ItemNumber = sku;
      Vendor = vendor;
      Description = description;
      PalletQuanity = palletQuanity;
      Barcode = barcode;
    }

    public string PatchName { get; }
    public int SourceRowIndex { get; }
    public string Section { get; }
    public int? BlockIndex { get; }
    public int? ItemNumber { get; }
    public string Vendor { get; }
    public string Description { get; }
    public string PalletQuanity { get; }
    public string Barcode { get; }

    public override string ToString()
    {
      return $"RowIndex: {SourceRowIndex}, Sku: {ItemNumber}, Description: {Description}";
    }
  }
}