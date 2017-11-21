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

  public interface IJob : IScanbookEntity<IJob, IBook>
  {
    IEnumerable<string> DataSourcePaths { get; }
    void AddDataSource(string dataSourcePath);
  }

  public interface IBook : IScanbookEntity<IJob, ISection>
  {
    int PageCount { get; }
  }

  public interface ISection : IScanbookEntity<IBook, IPage> { }

  public interface IPage : IScanbookEntity<IBook, IPatioBlok>
  {
    string Header { get; }
  }

  public interface IPatioBlok : IScanbookEntity<IPage, IPatioBlok>
  {
    int? ItemNumber { get; }
    string Vendor { get; }
    string PalletQuantity { get; }
    string Barcode { get; }
  }
}