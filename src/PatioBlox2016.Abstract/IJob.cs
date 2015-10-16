namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.Linq;

  public interface IJob : IJsxExportable
  {
    IReadOnlyCollection<IBook> Books { get; }
    void AddBook(IBook book);
    void AddBookRange(IEnumerable<IBook> books);
    void RemoveBook(IBook book);
    void ClearBooks();

    IReadOnlyCollection<IDescription> Descriptions { get; }
    void AddDescriptionRange(IEnumerable<IDescription> descriptions);
    void ClearDescriptions();

    void PopulateJob(IEnumerable<IGrouping<string, IPatchRowExtract>> bookGroups);
  }
}