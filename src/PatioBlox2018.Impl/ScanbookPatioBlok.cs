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

    public int ItemNumber => PatioBlokRow.ItemNumber.GetValueOrDefault();
    public string Vendor => PatioBlokRow.Vendor;
    public string Description => PatioBlokRow.Description;
    public string PalletQuantity => PatioBlokRow.PalletQuanity;
    public string Barcode => PatioBlokRow.Barcode;
    public int SourceRowIndex => PatioBlokRow.SourceRowIndex;

    public override string ToString()
    {
      return $"Description: {Description}";
    }
  }
}