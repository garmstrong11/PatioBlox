namespace PatioBlox2018.Impl {
  using System.Collections.Generic;

  public interface IBook {
    SortedSet<ScanbookPatioBlok> BlockSet { get; }
    IEnumerable<string> DuplicatePatioBloxWarnings { get; }
    int StoreCount { get; }
    int PageCount { get; }
    int SheetCount { get; }
    int SetsForPatch { get; }
    ScanbookJob Job { get; }
    string Name { get; }
    IEnumerable<ScanbookPage> Pages { get; }
    int GetBlockCount();
    IEnumerable<string> GetSheetNames();
  }
}