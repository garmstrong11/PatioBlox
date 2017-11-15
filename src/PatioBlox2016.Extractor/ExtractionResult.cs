namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using PatioBlox2018.Core;

  public class ExtractionResult : IExtractionResult
  {
    private readonly List<IPatchRowExtract> _patchRowExtracts;

    public ExtractionResult()
    {
      _patchRowExtracts = new List<IPatchRowExtract>();
    }

    public IEnumerable<IPatchRowExtract> PatchRowExtracts 
      => _patchRowExtracts.AsEnumerable();

    public void AddPatchRowExtractRange(IEnumerable<IPatchRowExtract> patchRowExtracts)
    {
      if (patchRowExtracts == null) throw new ArgumentNullException(nameof(patchRowExtracts));
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

    public IEnumerable<IGrouping<string, IPatchRowExtract>> BookGroups
    {
      get { return _patchRowExtracts.GroupBy(pr => pr.PatchName); }
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
  }
}