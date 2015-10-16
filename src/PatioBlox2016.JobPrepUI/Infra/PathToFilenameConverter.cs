namespace PatioBlox2016.JobPrepUI.Infra
{
  using System;
  using System.Globalization;
  using System.IO;
  using System.Windows.Data;

  public class PathToFilenameConverter : IValueConverter
  {
    #region Implementation of IValueConverter

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return string.IsNullOrWhiteSpace(value.ToString()) ? "NO FILE SELECTED" : Path.GetFileName(value.ToString());
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}