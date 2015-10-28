namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;
  using Concrete.Exceptions;
  using Services.Contracts;

  public class CellFactory : ICellFactory
  {
    private readonly IExtractionResultValidationUow _uow;

    public CellFactory(IExtractionResultValidationUow uow)
    {
      _uow = uow;
    }
    
    public Cell CreateCell(IPatchRowExtract extract, 
      Dictionary<string, int> descriptionDict, 
      Dictionary<string, string> upcReplacementDict)
    {
      int descriptionId;
      string upc;

      //var descriptionDict = _uow.GetDescriptionTextToIdDict();
      //var upcReplacementDict = _uow.GetUpcReplacementDictionary();

      var cell = new Cell(extract);

      var found = descriptionDict.TryGetValue(extract.Description, out descriptionId);

      if (!found) {
        var message = string.Format(
          "Unable to find a Description for the Cell at index {0}, Sku: {1}, Description: {2}",
          extract.RowIndex, extract.Sku, extract.Description);

        var exception = new CellConstructionException(
          extract.RowIndex, extract.Sku, extract.Description, message);

        throw exception;
      }

      cell.Upc = upcReplacementDict.TryGetValue(extract.Upc, out upc) 
        ? upc 
        : extract.Upc;

      cell.DescriptionId = descriptionId;

      return cell;
    }
  }
}