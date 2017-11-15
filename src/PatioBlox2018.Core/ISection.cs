namespace PatioBlox2018.Core
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  //public interface ISection
  //{
  //  string SectionName { get; }
  //  int SourceRowIndex { get; }
  //  IBook Book { get; }
  //  ReadOnlyCollection<ICell> Cells { get; }
  //  IReadOnlyList<IPage> Pages { get; }
  //  int PageCount { get; }
  //}

  // A job is root node (no parent node), and contains book nodes, no parent
  // A book node contains section nodes
  // A section node contains page nodes
  // A page node contains patio blok nodes
  // A patio blok is leaf node (no child nodes)

  public interface IScanbookEntity
  {
    int SourceRowIndex { get; }
    string Name { get; }
  }

  public interface IContainerEntity<out T> : IScanbookEntity
  {
    IEnumerable<T> Children { get; }
  }

  public interface IContainedEntity<out T> : IScanbookEntity
  {
    T Parent { get; }
  }

  public interface IBranchEntity<out TParent, out TChild> : IContainerEntity<TChild>, IContainedEntity<TParent>
  {
    
  }

  public interface IPatioBlok<out TParent> : IContainedEntity<TParent>
  {
    int ItemNumber { get; }
    string Vendor { get; }
    string Description { get; }
    int PalletQuantity { get; }
    string Barcode { get; }
  }
}