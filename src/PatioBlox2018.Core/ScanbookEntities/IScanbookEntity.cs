namespace PatioBlox2018.Core.ScanbookEntities
{
  using System.Collections.Generic;

  public interface IScanbookEntity<out TRoot, in TBranch> 
  {
    int SourceRowIndex { get; }
    string Name { get; }
    TRoot Root { get; }
    int BranchCount { get; }
    void AddBranch(IPatchRow patchRow);
    void AddBranches(IEnumerable<IPatchRow> patchRows);
    string GetJson();
  }

  public interface IJob
  {
    IEnumerable<IBook> Books { get; }
    IEnumerable<string> DataSourcePaths { get; }
    void AddDataSource(string dataSourcePath);
    int PageCount { get; }
    void AddBook(IBook book);
    string Name { get; }
    string GetJson();
  }

  public interface IBook
  {
    IJob Job { get; }
    IEnumerable<ISection> Sections { get; }
    int PageCount { get; }
    string Name { get; }
    void AddSection(ISection section);
  }

  public interface ISection
  {
    IBook Book { get; }
    IEnumerable<IPage> Pages { get; }
    int PageCount { get; }
    string Name { get; }
    int SourceRowIndex { get; }
    void AddPage(IPage page);
  }

  public interface IPage
  {
    ISection Section { get; }
    IEnumerable<IPatioBlok> PatioBlox { get; }
    string Header { get; }
    int BlockCount { get; }
    int SourceRowIndex { get; }
    void AddPatioBlok(IPatioBlok patioBlok);
  }

  public interface IPatioBlok
  {
    IPage Page { get; }
    int ItemNumber { get; }
    string Vendor { get; }
    string Description { get; }
    string PalletQuantity { get; }
    string Barcode { get; }
    int SourceRowIndex { get; }
  }
}