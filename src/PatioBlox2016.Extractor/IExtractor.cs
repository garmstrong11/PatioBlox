namespace PatioBlox2016.Extractor
{
  using System.Collections.Generic;

  public interface IExtractor<out T> where T : class
	{
		/// <summary>
		/// The full path of the Excel file from which to extract data.
		/// </summary>
		string SourcePath { get; }

		/// <summary>
		/// Prepares the extractor for extraction.
		/// <param name="path">The file path from which to extract data.</param>
		/// </summary>
		void Initialize(string path);

		/// <summary>
		/// Extracts a Dictionary of column names to indices from an Excel worksheet
		/// </summary>
		/// <param name="sheetIndex"></param>
		/// <param name="headerRowIndex"></param>
		/// <returns></returns>
		IDictionary<string, int> GetColumnMap(int sheetIndex, int headerRowIndex);

		/// <summary>
		/// Checks whether the extractor passed initialization.
		/// </summary>
		bool IsInitialized { get; }

    IEnumerable<T> Extract();
	}
}