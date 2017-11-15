namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Text.RegularExpressions;
  using FlexCel.Core;
  using FlexCel.XlsAdapter;
  using PatioBlox2018.Core;

  public class FlexCelDataSourceAdapter : IDataSourceAdapter
	{
		private readonly XlsFile _xls;
	  private string _sourcePath;

	  public FlexCelDataSourceAdapter()
		{
			_xls = new XlsFile();
		}

		public int GetRowColumnCount(int rowIndex)
		{
			if(rowIndex < 1) throw new ArgumentOutOfRangeException(nameof(rowIndex));
			return _xls.ColCountInRow(rowIndex);
		}

    public void SetActiveSheetByName(string sheetName) 
      => _xls.ActiveSheetByName = sheetName;

    public void Open(string sourcePath)
		{
		  if (string.IsNullOrWhiteSpace(sourcePath))
		    throw new ArgumentException("Value cannot be null or whitespace.", nameof(sourcePath));

		  _sourcePath = sourcePath;

			_xls.Open(_sourcePath);
			ActiveSheet = 1;
		}

		public int ActiveSheet
		{
			get => _xls.ActiveSheet;
		  set => _xls.ActiveSheet = value;
		}

		public int ColumnCount => _xls.ColCount;

	  public int RowCount => _xls.RowCount;

	  public int SheetCount => _xls.SheetCount;

	  public int GetRowCountForSheet(int sheetIndex) => _xls.GetRowCount(sheetIndex);

	  public int GetColumnCountForSheet(int sheetIndex) => _xls.GetColCount(sheetIndex);

    /// <summary>
    /// Use this method to extract string dates from date-formatted cells
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="columnIndex"></param>
    /// <returns></returns>
    public string ExtractRawString(int rowIndex, int columnIndex)
		{
			var richString = _xls.GetStringFromCell(rowIndex, columnIndex);

			return richString.ToString(CultureInfo.CurrentCulture).Trim();
		}

		public string ExtractDateString(int rowIndex, int columnIndex)
		{
			var xf = 0;
			var v = _xls.GetCellValue(rowIndex, columnIndex, ref xf);
			
			var exception = new FlexCelExtractionException(_xls.ActiveSheet, rowIndex, columnIndex)
			{
				FileSpec = _xls.ActiveFileName
			};

			if (v == null) {
				throw exception;
			}

			var vType = v.GetType();
			var vTypeCode = Type.GetTypeCode(vType);
			exception.Type = vType;

			if (vTypeCode != TypeCode.Double) {
				throw exception;
			}

			DateTime date;

			if (FlxDateTime.TryFromOADate((double) v, false, out date)) {
				return date.ToString("M/d/yyyy");
			}

			exception.Type = date.GetType();
			throw exception;
		}

		#region ExtractString

		public string ExtractString(int rowIndex, int columnIndex)
		{
			return CheckString(_xls.GetCellValue(rowIndex, columnIndex));
		}

		public string ExtractString(int sheetIndex, int rowIndex, int columnIndex)
		{
			var xf = 0;
			return CheckString(_xls.GetCellValue(sheetIndex, rowIndex, columnIndex, ref xf));
		}

		private static string CheckString(object val)
		{
		  if (val == null) return string.Empty;
      var extract = val.ToString().Trim().Replace("\n", " ");

		  return Regex.Replace(extract, @" {2,}", " ");
		}
		#endregion

		#region ExtractDouble
		public double? ExtractDouble(int rowIndex, int columnIndex)
		  => CheckDouble(_xls.GetCellValue(rowIndex, columnIndex));

		public double? ExtractDouble(int sheetIndex, int rowIndex, int columnIndex)
		{
			var xf = 0;
			return CheckDouble(_xls.GetCellValue(sheetIndex, rowIndex, columnIndex, ref xf));
		}

		public bool IsCellNull(int rowIndex, int columnIndex)
		{
			var extract = _xls.GetCellValue(rowIndex, columnIndex);

			return extract == null;
		}

		private static double? CheckDouble(object extract)
		{
			if (extract == null) return null;

		  if (!(extract is string)) return extract as double?;

      var str = extract.ToString().Trim();
      if (string.IsNullOrWhiteSpace(str)) return null;
		  if (double.TryParse(str, out double dbl)) return dbl;

		  return null;
		}

		#endregion

		#region ExtractInteger
		public int? ExtractInteger(int rowIndex, int columnIndex)
		{
			return CheckInteger(ExtractDouble(rowIndex, columnIndex));
		}

		public int? ExtractInteger(int sheetIndex, int rowIndex, int columnIndex)
		{
			return CheckInteger(ExtractDouble(sheetIndex, rowIndex, columnIndex));
		}

		private static int? CheckInteger(double? val)
		{
		  if (!val.HasValue) return null;
			return Convert.ToInt32(Math.Ceiling(val.Value));
		}
		#endregion

		public void Save(string path)
		{
			_xls.Save(path);
		}

		public void SetCellValue(int rowIndex, int columnIndex, object value)
			=>_xls.SetCellValue(rowIndex, columnIndex, value);

		public void MarkForAutofit(int columnIndex)
			=> _xls.MarkColForAutofit(columnIndex, true, 1.2);

		public void AutofitColumns()
			=> _xls.AutofitMarkedRowsAndCols(true, false, 1.0);

		public void FreezeTopRow() => _xls.FreezePanes(new TCellAddress(2, 2));

    public Tuple<byte, byte, byte> GetCellColor(int rowIndex, int columnIndex)
		{
		  var color = _xls.GetCellVisibleFormatDef(rowIndex, columnIndex)
        .FillPattern.FgColor.ToColor(_xls);

		  return new Tuple<byte, byte, byte>(color.R, color.G, color.B);
		}

	  public bool IsCellForegroundAutoColor(int rowIndex, int columnIndex)
	  {
	    return _xls.GetCellVisibleFormatDef(rowIndex, columnIndex)
	      .FillPattern.FgColor.IsAutomatic;
	  }

    public IEnumerable<string> GetSheetNames()
    {
      var patchCount = _xls.SheetCount;

      for (var i = 1; i <= patchCount; i++) {
        _xls.ActiveSheet = i;
        yield return _xls.SheetName;
      }
    }

    public string SheetName => _xls.SheetName;
	}
}