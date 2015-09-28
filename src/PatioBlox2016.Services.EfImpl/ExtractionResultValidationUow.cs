namespace PatioBlox2016.Services.EfImpl
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Data.Entity;
  using System.Linq;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.DataAccess;
  using PatioBlox2016.Extractor;
  using PatioBlox2016.Services.Contracts;

  public class ExtractionResultValidationUow : IExtractionResultValidationUow
  {
    private readonly PatioBloxContext _context;
    private readonly IExtractionResult _extractionResult;
    private readonly ObservableCollection<UpcReplacement> _upcReplacements;
    private readonly ObservableCollection<Description> _descriptions;
    private readonly ObservableCollection<Keyword> _keywords; 

    public ExtractionResultValidationUow(PatioBloxContext context, IExtractionResult extractionResult)
    {
      if (context == null) throw new ArgumentNullException("context");
      if (extractionResult == null) throw new ArgumentNullException("extractionResult");

      _context = context;
      _extractionResult = extractionResult;

      // Possibly move Load() calls to bootstrapper
      _context.Descriptions.Load();
      _context.Keywords.Load();
      _context.UpcReplacements.Load();

      _upcReplacements = _context.UpcReplacements.Local;
      _descriptions = _context.Descriptions.Local;
      _keywords = _context.Keywords.Local;
    }
    
    public IEnumerable<IProduct> GetProducts()
    {
      var upcDict = _upcReplacements.ToDictionary(k => k.InvalidUpc, v => v.Replacement);
      var extracts = _extractionResult.GetProductExtracts();
      var groops = extracts.GroupBy(g => new { g.Sku, g.Upc });

      // If the groop's Key.Upc is in upcDict, use that for the product's
      // Upc value. If not, use the incoming Upc from the PatchRowExtract.
      // Remember that the product's upc may still be invalid if it has 
      // not been corrected in the UpcReplacements table.
      foreach (var groop in groops)
      {
        string upcFromDict;
        var upcValue = upcDict.TryGetValue(groop.Key.Upc, out upcFromDict)
          ? upcFromDict
          : groop.Key.Upc;

        var prod = new Product(groop.Key.Sku, upcValue);
        foreach (var extract in groop)
        {
          prod.AddUsage(new UsageLocation(extract.PatchName, extract.RowIndex));
        }

        yield return prod;
      }
    }

    public void PersistUpcReplacementsForInvalidUpcs()
    {
      var presentUpcs = _upcReplacements.Select(u => u.InvalidUpc);
      var invalidUpcs = _extractionResult.InvalidUpcs;

      var missingUpcs = invalidUpcs
        .Except(presentUpcs)
        .Select(u => new UpcReplacement(u));

      _context.UpcReplacements.AddRange(missingUpcs);
      _context.SaveChanges();
    }

    public void PersistNewDescriptions()
    {
      var dbDescriptionTexts = _descriptions.Select(d => d.Text);
      var missingDescriptions = _extractionResult.UniqueDescriptions
        .Except(dbDescriptionTexts)
        .Select(d => new Description(d));

      _context.Descriptions.AddRange(missingDescriptions);
      _context.SaveChanges();
    }

    public void PersistNewKeywords()
    {
      var parent = _keywords.Single(k => k.Word == "NEW");
      var dbWords = _keywords.Select(k => k.Word);
      var missingWords = _extractionResult.UniqueWords
        .Except(dbWords)
        .Select(w => new Keyword(w) {Parent = parent});

      _context.Keywords.AddRange(missingWords);
      _context.SaveChanges();
    }
  }
}