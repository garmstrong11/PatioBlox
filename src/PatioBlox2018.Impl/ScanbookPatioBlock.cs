namespace PatioBlox2018.Impl
{
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public class ScanbookPatioBlock
  {
    public IPatchRow PatchRow { get; }
    public string Section { get; }

    
    public ScanbookPatioBlock(IPatchRow patchRow, string section)
    {
      PatchRow = patchRow;
      Section = section;
    }

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

    public int SourceRowIndex => PatchRow.SourceRowIndex;

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}