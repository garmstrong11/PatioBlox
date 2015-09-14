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
    private readonly IDictionary<string, Barcode> _barcodeDict; 
    private readonly IBarcodeCorrectionRepository _barcodeCorrectionRepository;

    public CellFactory(
      IDescriptionRepository descriptionRepository, 
      IBarcodeRepository barcodeRepository, 
      IBarcodeCorrectionRepository barcodeCorrectionRepository)
    {
      if (descriptionRepository == null) throw new ArgumentNullException("descriptionRepository");
      if (barcodeRepository == null) throw new ArgumentNullException("barcodeRepository");
      if (barcodeCorrectionRepository == null) throw new ArgumentNullException("barcodeCorrectionRepository");

      _descriptionDict = descriptionRepository.GetDescriptionDictionary();
      _barcodeDict = barcodeRepository.GetBarcodeDictionary();
      _barcodeCorrectionRepository = barcodeCorrectionRepository;
    }
    
    public Cell CreateCell(IPatchRowExtract extract)
    {
      var cell = new Cell();
      Description description;
      Barcode barcode;

      if (!_descriptionDict.TryGetValue(extract.Description, out description)) {
        throw new KeyNotFoundException(
          string.Format("Unable to find a description with the key {0}", extract.Description));
      }

      if (!_barcodeDict.TryGetValue(extract.Upc, out barcode)){
        throw new KeyNotFoundException(
          string.Format("Unable to find a barcode with the key {0}", extract.Upc));
      }

      cell.Upc = barcode.Upc;

      var correction = _barcodeCorrectionRepository.Get(barcode.Id);

      if (correction.Any()) {
        cell.Upc = correction.First().CorrectedValue;
      }

      cell.Description = description;
      cell.SourceRowIndex = extract.RowIndex;
      cell.Sku = extract.Sku;
      cell.PalletQty = extract.PalletQuanity;

      return cell;
    }
  }
}