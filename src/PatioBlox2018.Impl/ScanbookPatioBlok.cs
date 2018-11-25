namespace PatioBlox2018.Impl
{
  using System;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPatioBlok : ScanbookEntityBase<ScanbookPage, ScanbookPatioBlok>
  {
    public IBarcode Barcode { get; }
    private Func<int, ScanbookPage> ParentFinder { get; }

    public ScanbookPatioBlok(
      IPatchRow patioBlokRow, 
      Func<int, ScanbookPage> parentFinder,
      IBarcode barcode)
      : base(patioBlokRow, parentFinder)
    {
      Barcode = barcode;
      ParentFinder = parentFinder;
      Page.AddChild(this);
    }

    public ScanbookPage Page => ParentFinder(SourceRowIndex);

    [JsonProperty(PropertyName = "sku")]
    public int ItemNumber => PatchRow.ItemNumber.GetValueOrDefault();

    [JsonProperty(PropertyName = "vndr")]
    public string Vendor => PatchRow.Vendor;

    [JsonProperty(PropertyName = "desc")]
    public string Description => PatchRow.Description;

    [JsonProperty(PropertyName = "qty")]
    public string PalletQuantity => PatchRow.PalletQuanity;

    [JsonProperty(PropertyName = "upc")]
    public string Upc => Barcode.Value;

    public string PhotoFilename => $"{ItemNumber}.psd";

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}