namespace PatioBlox2018.Core.ScanbookEntities {
  using System.Collections.Generic;

  public interface ISection
  {
    IBook Book { get; }
    IEnumerable<IPage> Pages { get; }
    int PageCount { get; }
    string Name { get; }
    int SourceRowIndex { get; }
    void AddPage(IPage page);
  }
}