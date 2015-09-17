namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Abstract;
  using Concrete;

  public class ExtractionResult : IExtractionResult
  {
    private readonly List<IPatchRowExtract> _patchRowExtracts;

    public ExtractionResult()
    {
      _patchRowExtracts = new List<IPatchRowExtract>();
    }

    public IEnumerable<IPatchRowExtract> PatchRowExtracts
    {
      get { return _patchRowExtracts.AsEnumerable(); }
    }

    public void AddPatchRowExtract(IPatchRowExtract patchRowExtract)
    {
      if (patchRowExtract == null) throw new ArgumentNullException("patchRowExtract");
      _patchRowExtracts.Add(patchRowExtract);
    }

    public void AddPatchRowExtractRange(IEnumerable<IPatchRowExtract> patchRowExtracts)
    {
      if (patchRowExtracts == null) throw new ArgumentNullException("patchRowExtracts");
      _patchRowExtracts.AddRange(patchRowExtracts);
    }

    public IEnumerable<string> PatchNames
    {
      get { return _patchRowExtracts.Select(pr => pr.PatchName).Distinct(); }
    }

    public IEnumerable<string> UniqueDescriptions
    {
      get
      {
        var descriptionTexts = _patchRowExtracts
          .Where(pr => !string.IsNullOrWhiteSpace(pr.Description))
          .Select(pr => pr.Description).Distinct();

        return descriptionTexts;
      }
    }

    public IEnumerable<string> UniqueUpcs
    {
      get { return _patchRowExtracts
        .Where(pr => !string.IsNullOrWhiteSpace(pr.Upc) && pr.Sku > 0)
        .Select(pr => pr.Upc)
        .Distinct(); }
    }

    public IEnumerable<IProduct> UniqueProducts
    {
      get { return _patchRowExtracts
        .Where(pr => pr.Sku > 0)
        .Select(pr => new Product(pr.Sku, pr.Upc))
        .Distinct()
        .OrderBy(pr => pr.Sku); }
    } 

    public IEnumerable<string> UniqueSectionNames
    {
      get
      {
        return _patchRowExtracts
          .Where(pr => !string.IsNullOrWhiteSpace(pr.Section))
          .Where(pr => !Regex.IsMatch(pr.Section, @"^Page ?\d+$"))
          .Select(pr => pr.Section)
          .Distinct();
      }
    }

    public IEnumerable<string> UniqueWords
    {
      get
      {
        return UniqueDescriptions
          .SelectMany(Description.ExtractWordList)
          .Distinct()
          .OrderBy(d => d);
      }
    }

    public IEnumerable<int> UniqueSkus
    {
      get { return _patchRowExtracts.Select(pr => pr.Sku).Distinct(); }
    }

    public IEnumerable<string> InvalidUpcs
    {
      get
      {
        var badUpcs = from upc in UniqueUpcs
          let barcode = new Barcode(upc)
          where !barcode.IsValid
          select upc;

        return badUpcs;
      }
    }
  }
}