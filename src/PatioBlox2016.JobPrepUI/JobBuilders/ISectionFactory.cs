namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface ISectionFactory
  {
    IEnumerable<Section> CreateSections(IEnumerable<IPatchRowExtract> extracts, Book book);
  }
}