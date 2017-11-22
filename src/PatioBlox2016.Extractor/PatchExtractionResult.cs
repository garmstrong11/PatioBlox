namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Remoting.Messaging;
  using PatioBlox2018.Core;

  public class PatchExtractionResult : IExtractionResult
  {
    private List<IPatchRow> Extracts { get; }

    public PatchExtractionResult()
    {
      Extracts = new List<IPatchRow>();
    }

    public IEnumerable<IPatchRow> PatchRowExtracts => Extracts.AsEnumerable();

    public void AddPatchRowExtractRange(IEnumerable<IPatchRow> patchRowExtracts)
    {
      if (patchRowExtracts == null)
        throw new ArgumentNullException(nameof(patchRowExtracts));

      Extracts.AddRange(patchRowExtracts);
    }

    public IEnumerable<string> PatchNames
    {
      get { return Extracts.Select(pr => pr.PatchName).Distinct(); }
    }

    public IEnumerable<IPatchRow> GetRowsForPatch(string patchName)
    {
      if (string.IsNullOrWhiteSpace(patchName))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(patchName));

      var patchRows = RowsByPatch
        .FirstOrDefault(p => p.Key == patchName)
        ?? throw new ArgumentException($"Requested patch {patchName} contains no rows.");

      return patchRows.OrderBy(p => p.SourceRowIndex);
    }

    private IEnumerable<IGrouping<string, IPatchRow>> RowsByPatch
      => Extracts.GroupBy(pr => pr.PatchName);  
  }
}