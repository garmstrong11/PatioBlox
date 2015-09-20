namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IPageFactory
  {
    IEnumerable<Page> CreatePages(Section section, IEnumerable<IPatchRowExtract> extracts);
  }
}