namespace PatioBlox2018.Core
{
  /// <summary>
  /// Encapsulates important column indices for extracting
  /// values from Excel Spreadsheets.
  /// </summary>
	public interface IColumnIndexService
	{
    /// <summary>
    /// The spreadsheet column index at which the Section data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int SectionIndex { get; }

    /// <summary>
    /// The spreadsheet column index at which the ItemNumber data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int ItemNumberIndex { get; }

    /// <summary>
    /// The spreadsheet column index at which the Description data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int DescriptionIndex { get; }

    /// <summary>
    /// The spreadsheet column index at which the PalletQuantity data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int PalletQtyIndex { get; }

    /// <summary>
    /// The spreadsheet column index at which the Upc data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int BarcodeIndex { get; }

    /// <summary>
    /// The spreadsheet column index at which the PageOrder data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int PageOrderIndex { get; }

    /// <summary>
    /// The spreadsheet column index at which the Vendor data can be found
    /// in the Patio Blocks Patches spreadsheet.
    /// </summary>
    int VendorIndex { get; }

	  /// <summary>
	  /// The spreadsheet column index at which the PatchArea data can be found
	  /// in the Store List spreadsheet.
	  /// </summary>
    int PatchAreaIndex { get; }

	  /// <summary>
	  /// The spreadsheet column index at which the StoreId data can be found
	  /// in the Store List spreadsheet.
	  /// </summary>
    int StoreIdIndex { get; }
	}
}