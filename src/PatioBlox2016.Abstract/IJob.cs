namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.Linq;

  public interface IJob : IJsxExportable
  {
    IReadOnlyCollection<IBook> Books { get; }
    void PopulateBooks(IEnumerable<IGrouping<string, IPatchRowExtract>> bookGroups);
    void ClearBooks();

    IReadOnlyCollection<IDescription> Descriptions { get; }
    void AddDescriptionRange(IEnumerable<IDescription> descriptions);
    void ClearDescriptions();

    IReadOnlyCollection<IProduct> Products { get; }
    void PopulateProducts(IEnumerable<IProduct> products);
    void ClearProducts();
  }
}