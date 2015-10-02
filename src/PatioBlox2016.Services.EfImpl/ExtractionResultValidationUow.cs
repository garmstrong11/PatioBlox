namespace PatioBlox2016.Services.EfImpl
{
  using System;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Data.Entity;
  using System.Linq;
  using Abstract;
  using Concrete;
  using DataAccess;
  using Extractor;
  using Services.Contracts;

  public class ExtractionResultValidationUow : IExtractionResultValidationUow
  {
    private readonly PatioBloxContext _context;
    private readonly IExtractionResult _extractionResult;
    private readonly ObservableCollection<UpcReplacement> _upcReplacements;
    private readonly ObservableCollection<Description> _descriptions;
    private readonly ObservableCollection<Keyword> _keywords;

    private const string DefaultName = "NEW";

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

    public void PersistNewUpcReplacements()
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
      var now = DateTime.Now;
      var dbDescriptionTexts = _descriptions.Select(d => d.Text);
      var missingDescriptions = _extractionResult.UniqueDescriptions
        .Except(dbDescriptionTexts)
        .Select(d => new Description(d){InsertDate = now});

      _context.Descriptions.AddRange(missingDescriptions);
      _context.SaveChanges();
    }

    public void PersistNewKeywords()
    {
      var parent = _keywords.Single(k => k.Word == DefaultName);
      var dbWords = _keywords.Select(k => k.Word);
      var missingWords = _extractionResult.UniqueWords
        .Except(dbWords)
        .Select(w => new Keyword(w) {Parent = parent});

      _context.Keywords.AddRange(missingWords);
      _context.SaveChanges();
    }

    public ObservableCollection<Keyword> GetKeywords()
    {
      return _keywords;
    }

    public void AddKeyword(Keyword keyword)
    {
      _keywords.Add(keyword);
    }

    public List<Keyword> GetRootKeywords()
    {
      return _keywords
        .Where(k => k.Parent == null)
        .ToList();
    }

    public List<Keyword> GetParentKeywords()
    {
      var rootWords = GetRootKeywords()
        .Select(k => k.Word);

      return _keywords
        .Where(k => k.Parent != null)
        .Where(k => rootWords.Contains(k.Parent.Word))
        .ToList();
    }

    public Keyword GetDefaultParent()
    {
      return _keywords.Single(k => k.Word == DefaultName);
    }

    public List<Keyword> GetNewKeywords()
    {
      var nonRootKeywords = _keywords.Where(k => k.Parent != null);
      return nonRootKeywords
        .Where(k => k.Parent.Word == DefaultName)
        .ToList();
    }

    public List<string> GetUsagesForWord(string word)
    {
      return _extractionResult.UniqueDescriptions
        .Where(d => Description.ExtractWordList(d).Contains(word))
        .ToList();
    }

    public Dictionary<string, Keyword> GetKeywordDict()
    {
      return _keywords.ToDictionary(k => k.Word);
    }

    public int SaveChanges()
    {
      return _context.SaveChanges();
    }

    public int GetPatchCount()
    {
      return _extractionResult.PatchNames.Count();
    }

    public int GetUniqueDescriptionCount()
    {
      return _extractionResult.UniqueDescriptions.Count();
    }

    public List<Description> GetUnresolvedDescriptions()
    {
      return _descriptions.Where(d => d.IsUnresolved).ToList();
    }

    public IEnumerable<Description> GetDescriptionsForJob()
    {
      return _descriptions.Where(d => _extractionResult.UniqueDescriptions.Contains(d.Text));
    } 

    public IEnumerable<int> GetUniqueSkus()
    {
      return _extractionResult.PatchRowExtracts
        .Where(pr => pr.Sku >= 0)
        .Select(pr => pr.Sku)
        .Distinct();
    }

    public IEnumerable<IPatchProductDuplicate> GetPatchProductDuplicates()
    {
      var dupeProducts = GetProducts().Where(p => p.HasPatchProductDuplicates);

      foreach (var dupeProduct in dupeProducts) {
        var dupeSets = dupeProduct.PatchProductDuplicates;
        foreach (var dupeSet in dupeSets) {
          var patchProductDuplicate = new PatchProductDuplicate(dupeProduct.Sku, dupeProduct.Upc);
          patchProductDuplicate.PatchName = dupeSet.First().PatchName;
          patchProductDuplicate.AddIndexRange(dupeSet.Select(d => d.RowIndex));

          yield return patchProductDuplicate;
        }
      }
    }

    public IEnumerable<IGrouping<string, IPatchRowExtract>> GetBookGroups()
    {
      return _extractionResult.BookGroups;
    }
  }
}