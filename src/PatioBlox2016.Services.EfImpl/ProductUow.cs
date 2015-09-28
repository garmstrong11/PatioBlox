namespace PatioBlox2016.Services.EfImpl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;
  using Concrete;
  using Contracts;
  using Extractor;

  public class ProductUow : IProductUow
  {
    private readonly IExtractionResult _extractionResult;
    private readonly IUpcReplacementRepository _upcReplacementRepository;

    public ProductUow(IExtractionResult extractionResult, IUpcReplacementRepository upcReplacementRepository)
    {
      if (extractionResult == null) throw new ArgumentNullException("extractionResult");
      if (upcReplacementRepository == null) throw new ArgumentNullException("upcReplacementRepository");

      _extractionResult = extractionResult;
      _upcReplacementRepository = upcReplacementRepository;
    }
    
    public IEnumerable<IProduct> GetProducts()
    {
      // Get a dictionary of upcReplacements from the db. Some of them may 
      // still be invalid, the point here is to seek upc values from the 
      // replacement dict if they are invalid in the extraction result.
      var upcDict = _upcReplacementRepository.GetUpcReplacementDictionary();
      var invalidBarcodes = _extractionResult.InvalidBarcodes;

      // Find invalid upcs in extraction result that do not exist in the 
      // upcReplacement table:
      var newReplacements = invalidBarcodes
        .Where(bc => !upcDict.ContainsKey(bc.Upc))
        .Select(bc => new UpcReplacement(bc.Upc));

      // Add new replacements to the upcReplacement
      // table, also add them to upcDict:
      foreach (var repl in newReplacements) {
        _upcReplacementRepository.Add(repl);
        upcDict.Add(repl.InvalidUpc, repl.Replacement);
      }

      // Save the new replacements (if any) to db
      // this will be no-op if no additions have been made
      _upcReplacementRepository.SaveChanges();

      var extracts = _extractionResult.GetProductExtracts();
      var groops = extracts.GroupBy(g => new { g.Sku, g.Upc });

      // If the groop's Key.Upc is in upcDict, use that for the product's
      // Upc value. If not, use the incoming Upc from the PatchRowExtract.
      // Remember that the product's upc may still be invalid if it has 
      // not been corrected in the UpcReplacements table.
      foreach (var groop in groops) {
        string upcFromDict;
        var upcValue = upcDict.TryGetValue(groop.Key.Upc, out upcFromDict) 
          ? upcFromDict 
          : groop.Key.Upc;

        var prod = new Product(groop.Key.Sku, upcValue);
        foreach (var extract in groop) {
          prod.AddUsage(new UsageLocation(extract.PatchName, extract.RowIndex));
        }

        yield return prod;
      }
    }
  }
}