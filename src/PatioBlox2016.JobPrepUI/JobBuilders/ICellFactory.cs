namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface ICellFactory
  {
    Cell CreateCell(IPatchRowExtract extract,
      Dictionary<string, int> descriptionDict,
      Dictionary<string, string> upcReplacementDict);
  }
}