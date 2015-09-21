namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using Abstract;
  using Concrete;
  using Services.Contracts;

  public class CellFactory : ICellFactory
  {
    private readonly IDictionary<string, Description> _descriptionDict;
    private readonly IDictionary<string, string> _upcReplacementDict;

    public CellFactory(
      IDescriptionRepository descriptionRepository,
      IUpcReplacementRepository upcReplacementRepository)
    {
      if (descriptionRepository == null) throw new ArgumentNullException("descriptionRepository");
      if (upcReplacementRepository == null) throw new ArgumentNullException("upcReplacementRepository");

      _descriptionDict = descriptionRepository.GetDescriptionDictionary();
      _upcReplacementDict = upcReplacementRepository.GetUpcReplacementDictionary();
    }
    
    public Cell CreateCell(Page page, IPatchRowExtract extract)
    {
      Description description;
      string upc;

      var found = _descriptionDict.TryGetValue(extract.Description, out description);

      if (!found) {
        throw new KeyNotFoundException(
          string.Format("Unable to find a description with the key {0}", extract.Description));
      }

      var cell = new Cell(page, extract.RowIndex, extract.Sku, extract.PalletQuanity, extract.Description);

      cell.Upc = _upcReplacementDict.TryGetValue(extract.Upc, out upc) 
        ? upc 
        : extract.Upc;

      cell.Name = string.IsNullOrWhiteSpace(description.Vendor)
        ? description.Name
        : string.Format("{0}|{1}", description.Vendor, description.Name);

      cell.Color = description.Color;
      cell.Size = description.Size;

      return cell;
    }
  }
}