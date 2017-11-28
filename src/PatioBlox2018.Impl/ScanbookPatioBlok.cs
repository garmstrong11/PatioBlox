namespace PatioBlox2018.Impl
{
  using System;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPatioBlok : ScanbookEntityBase<ScanbookPage, ScanbookPatioBlok>
  {
    public ScanbookPatioBlok(IPatchRow patioBlokRow, Func<int, ScanbookPage> parentFinder)
      : base(patioBlokRow, parentFinder)
    {
      Page = parentFinder(SourceRowIndex);
      Page.AddChild(this);
    }

    public ScanbookPage Page { get; }

    [JsonProperty(PropertyName = "sku")]
    public int ItemNumber => PatchRow.ItemNumber.GetValueOrDefault();

    [JsonProperty(PropertyName = "vndr")]
    public string Vendor => PatchRow.Vendor;

    [JsonProperty(PropertyName = "desc")]
    public string Description => PatchRow.Description;

    [JsonProperty(PropertyName = "qty")]
    public string PalletQuantity => PatchRow.PalletQuanity;

    // TODO: Validate bar code
    [JsonProperty(PropertyName = "upc")]
    public string Barcode => PatchRow.Barcode;

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}