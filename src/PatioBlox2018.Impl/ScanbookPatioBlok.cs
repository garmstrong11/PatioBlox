namespace PatioBlox2018.Impl
{
  using System;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPatioBlok : ScanbookEntityBase<ScanbookPage, ScanbookPatioBlok>
  {
    private Func<int, string, IBarcode> BarcodeFactory { get; }
    private Func<int, ScanbookPage> ParentFinder { get; }

    public ScanbookPatioBlok(
      IPatchRow patioBlokRow, 
      Func<int, ScanbookPage> parentFinder,
      Func<int, string, IBarcode> barcodeFactory)
      : base(patioBlokRow, parentFinder)
    {
      BarcodeFactory = barcodeFactory;
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

    public IBarcode Barcode => BarcodeFactory(ItemNumber, PatchRow.Barcode);

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}