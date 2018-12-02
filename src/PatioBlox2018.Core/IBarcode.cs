namespace PatioBlox2018.Core
{
  using System.Collections.Generic;

  public interface IBarcode
  {
    /// <summary>
    /// A representation of the barcode's state.
    /// </summary>
    string Value { get; }

    string Coordinates { get; }

    /// <summary>
    /// The string submitted during the construction of this instance.
    /// </summary>
    string Candidate { get; }
  }
}