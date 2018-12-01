namespace PatioBlox2018.Impl
{
  using System;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPatioBlock : ScanbookEntityBase<ScanbookPage, ScanbookPatioBlock>
  {
    public IBarcode Barcode { get; }
    private Func<int, ScanbookPage> ParentFinder { get; }

    public ScanbookPatioBlock(
      IPatchRow patioBlockRows, 
      Func<int, ScanbookPage> parentFinder,
      IBarcode barcode)
      : base(patioBlockRows, parentFinder)
    {
      ParentFinder = parentFinder;
      Page.AddChild(this);
      Barcode = barcode;
      Barcode.AddUsage(Page.Section.Book.Name);
    }

    public ScanbookPage Page => ParentFinder(SourceRowIndex);

    [JsonProperty(PropertyName = "sku")]
    public int ItemNumber => PatchRows.ItemNumber.GetValueOrDefault();

    [JsonProperty(PropertyName = "vndr")]
    public string Vendor => PatchRows.Vendor;

    [JsonProperty(PropertyName = "desc")]
    public string Description => PatchRows.Description;

    [JsonProperty(PropertyName = "qty")]
    public string PalletQuantity => PatchRows.PalletQuantity;

    [JsonProperty(PropertyName = "upc")]
    public string Upc => Barcode.Value;

    public string PhotoFilename => $"{ItemNumber}.psd";

    public override string ToString() => $"Description: {Description}";
  }
}