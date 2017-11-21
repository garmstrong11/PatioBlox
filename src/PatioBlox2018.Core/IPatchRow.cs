namespace PatioBlox2018.Core
{
  public interface IPatchRow
  {
    /// <summary>
    /// The value extracted from the "Scanbook Section" column.
    /// </summary>
    string Section { get; }

    /// <summary>
    /// The value extracted from the "Order of Items on Page" column.
    /// </summary>
    int? BlockIndex { get; }

    /// <summary>
    /// The value extracted from the "Item #" column.
    /// </summary>
    int? ItemNumber { get; }

    /// <summary>
    /// The value extracted from the "Vendor" column.
    /// </summary>
    string Vendor { get; }

    /// <summary>
    /// The value extracted from the "Description" column.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// The value extracted from the "Pallet Qty" column
    /// </summary>
    string PalletQuanity { get; }

    /// <summary>
    /// The value extracted from the "Barcode" column.
    /// </summary>
    string Barcode { get; }

    /// <summary>
		/// The value extracted from the source worksheet's name.
		/// </summary>
    string PatchName { get; }

    /// <summary>
		/// The index of the source row.
		/// </summary>
    int SourceRowIndex { get; }
  }
}