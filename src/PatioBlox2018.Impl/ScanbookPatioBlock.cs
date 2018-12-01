namespace PatioBlox2018.Impl
{
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPatioBlock
  {
    public IPatchRow PatchRow { get; }

    public ScanbookPatioBlock(IPatchRow patchRow) => PatchRow = patchRow;

    [JsonProperty(PropertyName = "sku")]
    public int ItemNumber => PatchRow.ItemNumber.GetValueOrDefault();

    [JsonProperty(PropertyName = "vndr")]
    public string Vendor => PatchRow.Vendor;

    [JsonProperty(PropertyName = "desc")]
    public string Description => PatchRow.Description;

    [JsonProperty(PropertyName = "qty")]
    public string PalletQuantity => PatchRow.PalletQuantity;

    [JsonProperty(PropertyName = "upc")]
    public string Upc => PatchRow.Upc;

    public string PhotoFilename => $"{ItemNumber}.psd";

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}