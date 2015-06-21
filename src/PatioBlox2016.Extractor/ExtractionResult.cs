namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

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
      var isAdded = _patchDataFiles.Add(dataFile);

      if (!isAdded) {
        throw new InvalidOperationException("The data file already exists in the collection");
      }
    }

    public void AddPatchRowExtract(PatchRowExtract patchRowExtract)
    {
      _patchRowExtracts.Add(patchRowExtract);
    }

    public IEnumerable<string> GetAllPatchNames()
    {
      return _patchDataFiles.SelectMany(p => p.SheetNames);
    } 
  }
}