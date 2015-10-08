namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;
  using Abstract;

  public interface IPatchExtractor : IExtractor<IPatchRowExtract>
  {
    IEnumerable<string> ExtractPatchNames();
	  IEnumerable<IPatchRowExtract> ExtractOnePatch(string patchName);
  }
}