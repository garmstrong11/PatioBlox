namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IKeyword {
    int Id { get; set; }
    string Word { get; set; }
    int? ParentId { get; set; }
    IKeyword Parent { get; set; }
    ICollection<IKeyword> Members { get; set; }
    string RootWord { get; }
    string Expansion { get; }
  }
}