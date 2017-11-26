namespace PatioBlox2018.Core.ScanbookEntities {
  using System.Collections.Generic;

  public interface IBook
  {
    IJob Job { get; }
    IEnumerable<ISection> Sections { get; }
    IEnumerable<IPage> Pages { get; }
    int PageCount { get; }
    string Name { get; }
    void AddSection(ISection section);
  }
}