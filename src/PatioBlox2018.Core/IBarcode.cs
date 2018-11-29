namespace PatioBlox2018.Core
{
  using System.Collections.Generic;

  public interface IBarcode
  {
    /// <summary>
    /// A representation of the barcode's state.
    /// </summary>
    string Value { get; }

    /// <summary>
    /// A set of names of the books that use this instance.
    /// </summary>
    ISet<string> Usages { get; }

    /// <summary>
    /// Add a book name to the usage set
    /// </summary>
    /// <param name="bookName"></param>
    void AddUsage(string bookName);

    /// <summary>
    /// The string submitted during the construction of this instance.
    /// </summary>
    string Candidate { get; }
  }
}