namespace PatioBlox2016.Services.EfImpl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;
  using Concrete;
  using Contracts;

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
    
    public Cell CreateCell(IPatchRowExtract extract)
    {
      var cell = new Cell();
      Description description;
      string upc;

      if (!_descriptionDict.TryGetValue(extract.Description, out description)) {
        throw new KeyNotFoundException(
          string.Format("Unable to find a description with the key {0}", extract.Description));
      }

      cell.Upc = _upcReplacementDict.TryGetValue(extract.Upc, out upc) 
        ? upc 
        : extract.Upc;

      cell.Name = string.IsNullOrWhiteSpace(description.Vendor)
        ? description.Name
        : string.Format("{0}|{1}", description.Vendor, description.Name);

      cell.Color = description.Color;
      cell.Size = description.Size;
      cell.SourceRowIndex = extract.RowIndex;
      cell.Sku = extract.Sku;
      cell.PalletQty = extract.PalletQuanity;

      return cell;
    }
  }
}