namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IBookFactory
  {
    Book CreateBook(Job job, string name, IEnumerable<IPatchRowExtract> extracts);
  }
}