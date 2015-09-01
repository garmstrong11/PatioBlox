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
    private readonly IDescriptionFactory _descriptionFactory;
    private readonly List<IPatchRowExtract> _patchRowExtracts;

    public ExtractionResult(IDescriptionFactory descriptionFactory)
    {
      _descriptionFactory = descriptionFactory;
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

    public IEnumerable<Description> UniqueDescriptions
    {
      get
      {
        var descriptionTexts = _patchRowExtracts
          .Where(pr => !string.IsNullOrWhiteSpace(pr.Description))
          .Select(pr => pr.Description).Distinct();

        return descriptionTexts
          .Select(d => (Description) _descriptionFactory.CreateDescription(d));
      }
    }

    public IEnumerable<string> UniqueUpcs
    {
      get { return _patchRowExtracts
        .Where(pr => !string.IsNullOrWhiteSpace(pr.Upc))
        .Select(pr => pr.Upc)
        .Distinct(); }
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

    //public IEnumerable<string> UniqueWords
    //{
    //  get
    //  {
    //    var descriptionsNoSize = UniqueDescriptions
    //      .Select(d => Description.ExtractRemainder(d).ToUpper());

    //    return descriptionsNoSize
    //      .SelectMany(w => w.Split(new[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries))
    //      .Distinct()
    //      .OrderBy(w => w);
    //  }
    //}

    public IEnumerable<string> UniqueWords
    {
      get
      {
        return UniqueDescriptions
          .SelectMany(w => w.WordList)
          .Distinct()
          .OrderBy(d => d);
      }
    } 
  }
}