using System.Text.RegularExpressions;

namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Concrete;

  public class ExtractionResult : IExtractionResult
  {
    private readonly ISet<PatchDataFile> _patchDataFiles;
    private readonly List<PatchRowExtract> _patchRowExtracts;

    public ExtractionResult()
    {
      _patchDataFiles = new HashSet<PatchDataFile>();
      _patchRowExtracts = new List<PatchRowExtract>();
    }

    public IEnumerable<PatchDataFile> PatchDataFiles
    {
      get { return new List<PatchDataFile>(_patchDataFiles); }
    }

    public IEnumerable<PatchRowExtract> PatchRowExtracts
    {
      get { return _patchRowExtracts.AsEnumerable(); }
    }

    public void AddDataFile(PatchDataFile dataFile)
    {
      if (dataFile == null) throw new ArgumentNullException("dataFile");
      var isAdded = _patchDataFiles.Add(dataFile);

      if (!isAdded) {
        throw new InvalidOperationException("The data file already exists in the collection");
      }
    }

    public void AddPatchRowExtract(PatchRowExtract patchRowExtract)
    {
      if (patchRowExtract == null) throw new ArgumentNullException("patchRowExtract");
      _patchRowExtracts.Add(patchRowExtract);
    }

    public void AddPatchRowExtractRange(IEnumerable<PatchRowExtract> patchRowExtracts)
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
      get { return _patchRowExtracts
        .Where(pr => !string.IsNullOrWhiteSpace(pr.Description))
        .Select(pr => pr.Description).Distinct(); }
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
          .Select(pr => pr.Section)
          .Distinct();
      }
    }

    public IEnumerable<string> UniqueWords
    {
      get
      {
        var descriptionsNoSize = UniqueDescriptions
          .Select(d => Description.ExtractRemainder(d).ToUpper());

        return descriptionsNoSize
          .SelectMany(w => w.Split(new[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries))
          .Distinct()
          .OrderBy(w => w);
      }
    }
  }
}