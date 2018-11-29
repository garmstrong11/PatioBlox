namespace PatioBlox2018.Impl
{
  using System;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  public class ScanbookPatioBlok : ScanbookEntityBase<ScanbookSection, ScanbookPatioBlok>
  {
    public IBarcode Barcode { get; }
    private Func<int, ScanbookSection> ParentFinder { get; }

    public ScanbookPatioBlok(
      IPatchRow patioBlokRow, 
      Func<int, ScanbookSection> parentFinder,
      IBarcode barcode)
      : base(patioBlokRow, parentFinder)
    {
      ParentFinder = parentFinder;
      Section.AddChild(this);
      Barcode = barcode;
      Barcode.AddUsage(Section.Book.Name);
    }

    public ScanbookSection Section => ParentFinder(SourceRowIndex);

    [JsonProperty(PropertyName = "sku")]
    public int ItemNumber => PatchRow.ItemNumber.GetValueOrDefault();

    [JsonProperty(PropertyName = "vndr")]
    public string Vendor => PatchRow.Vendor;

    [JsonProperty(PropertyName = "desc")]
    public string Description => PatchRow.Description;

    [JsonProperty(PropertyName = "qty")]
    public string PalletQuantity => PatchRow.PalletQuantity;

    [JsonProperty(PropertyName = "upc")]
    public string Upc => Barcode.Value;

    public string PhotoFilename => $"{ItemNumber}.psd";

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}