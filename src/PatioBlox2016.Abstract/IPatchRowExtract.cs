namespace PatioBlox2016.Abstract
{
  public interface IPatchRowExtract
  {
    /// <summary>
    /// The value extracted from the Item # column.
    /// </summary>
    int Sku { get; }

    /// <summary>
    /// The value extracTed from the Description column.
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

    string PatchName { get; }

    int RowIndex { get; }
  }
}