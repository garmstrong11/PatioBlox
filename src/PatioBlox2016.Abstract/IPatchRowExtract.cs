namespace PatioBlox2016.Abstract
{
  public interface IPatchRowExtract
  {
    /// <summary>
    /// The value extracted from the Item # column.
    /// </summary>
    int Sku { get; }

    /// <summary>
    /// The value extracted from the Description column.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// The value extracted from the Scanbook Section column.
    /// </summary>
    string Section { get; }

    /// <summary>
    /// The value extracted from the Pallet Qty column
    /// </summary>
    string PalletQuanity { get; }

    /// <summary>
    /// The UPC string extracted from the Barcode column.
    /// </summary>
    string Upc { get; }

		/// <summary>
		/// The name of the source worksheet for this object.
		/// </summary>
    string PatchName { get; }

		/// <summary>
		/// The index of the source row for this object.
		/// </summary>
    int RowIndex { get; }
  }
}