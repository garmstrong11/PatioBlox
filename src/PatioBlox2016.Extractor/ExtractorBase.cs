namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.IO.Abstractions;
  using Abstract;

  public abstract class ExtractorBase<T> : IExtractor<T> where T : class
	{
		protected readonly IDataSourceAdapter XlAdapter;
	  protected readonly IFileSystem FileSystem;

	  protected ExtractorBase(IDataSourceAdapter adapter, IFileSystem fileSystem)
	  {
			if (adapter == null) throw new ArgumentNullException("adapter");
		  if (fileSystem == null) throw new ArgumentNullException("fileSystem");

		  XlAdapter = adapter;
		  FileSystem = fileSystem;
	  }

	  public string SourcePath { get; protected set; }

	  public virtual void Initialize(string path)
	  {
			if (string.IsNullOrWhiteSpace(path))
				throw new ArgumentNullException("path");

			if (!FileSystem.File.Exists(path))
				throw new FileNotFoundException(string.Format("The file '{0}' could not be found.", path));

		  SourcePath = path;
			XlAdapter.Open(SourcePath);
	  }

		public IDictionary<string, int> GetColumnMap(int sheetIndex, int headerRowIndex)
		{
			if (sheetIndex < 1 || sheetIndex > XlAdapter.SheetCount) {
				throw new ArgumentException("A sheet with the specified index does not exist", "sheetIndex");
			}

			XlAdapter.ActiveSheet = sheetIndex;
			var rowCount = XlAdapter.RowCount;

			if (headerRowIndex < 1 || headerRowIndex > rowCount) {
				throw new ArgumentException("The specified row index does not exist", "headerRowIndex");
			}

			var result = new Dictionary<string, int>();

			for (var i = 1; i <= XlAdapter.ColumnCount; i++)
			{
				var columnName = XlAdapter.ExtractString(headerRowIndex, i).Trim();
				if (result.ContainsKey(columnName)) continue;

				result.Add(XlAdapter.ExtractString(headerRowIndex, i), i);
			}

			return result;
		}

		public bool IsInitialized { get; protected set; }

    public abstract IEnumerable<T> Extract();
	}
}