namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IBookFactory
  {
    IBook CreateBook(IEnumerable<IPatchRowExtract> extracts);
  }
}