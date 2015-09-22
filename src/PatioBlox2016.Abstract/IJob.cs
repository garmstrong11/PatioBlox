namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IJob : IJsxExportable
  {
    IReadOnlyCollection<IBook> Books { get; }
    void AddBook(IBook book);
    void AddBookRange(IEnumerable<IBook> books);
    void RemoveBook(IBook book);
  }
}