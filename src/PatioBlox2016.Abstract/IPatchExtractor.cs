namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IPatchExtractor : IExtractor<IPatchRowExtract>
  {
    IEnumerable<string> ExtractPatchNames();
	  IEnumerable<IPatchRowExtract> ExtractOnePatch(string patchName);
  }
}