namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using Abstract;
  using Concrete;
  using PatioBlox2016.Concrete.Exceptions;
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
    
    public Cell CreateCell(IPatchRowExtract extract)
    {
      Description description;
      string upc;

      var cell = new Cell(extract);

      var found = _descriptionDict.TryGetValue(extract.Description, out description);

      if (!found) {
        var message = string.Format(
        "Unable to find a Description for the Cell at index {0}, Sku: {1}, Description: {2}",
        extract.RowIndex, extract.Sku, extract.Description);

        var exception = new CellConstructionException(extract.RowIndex, extract.Sku, extract.Description, message);

        throw exception;
      }

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