namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookPatioBlok : IPatioBlok
  {
    private IPatchRow PatioBlokRow { get; }

    public ScanbookPatioBlok(IPatchRow patioBlokRow, IEnumerable<IPage> pages)
    {
      PatioBlokRow = patioBlokRow ?? throw new ArgumentNullException(nameof(patioBlokRow));

      if (PatioBlokRow.ItemNumber == null)
        throw new ArgumentException("Row does not contain an ItemNumber", nameof(patioBlokRow));

      Page = pages?.Last(pg => pg.SourceRowIndex <= PatioBlokRow.SourceRowIndex) 
        ?? throw new ArgumentNullException(nameof(pages));

      Page.AddPatioBlok(this);
    }

    [JsonIgnore]
    public IPage Page { get; }

    [JsonProperty(PropertyName = "sku")]
    public int ItemNumber => PatioBlokRow.ItemNumber.GetValueOrDefault();

    [JsonProperty(PropertyName = "vndr")]
    public string Vendor => PatioBlokRow.Vendor;

    [JsonProperty(PropertyName = "desc")]
    public string Description => PatioBlokRow.Description;

    [JsonProperty(PropertyName = "qty")]
    public string PalletQuantity => PatioBlokRow.PalletQuanity;

    // TODO: Validate bar code
    [JsonProperty(PropertyName = "upc")]
    public string Barcode => PatioBlokRow.Barcode;

    [JsonIgnore]
    public int SourceRowIndex => PatioBlokRow.SourceRowIndex;

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}