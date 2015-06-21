namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using Abstract;

  public interface IPatchExtractor : IExtractor<IPatchRowExtract>
  {
    IEnumerable<string> ExtractPatchNames();
    int FindHeaderRow();
    void ChangeCurrentPatch(string patchName);
  }
}