namespace PatioBlox2018.Core.ScanbookEntities {
  using System.Collections.Generic;

  public interface IJob
  {
    IDictionary<string, IBook> Books { get; }
    int PageCount { get; }
    void AddBook(IBook book);
    string Name { get; }
    void BuildBooks();
    string GetJson();
  }
}