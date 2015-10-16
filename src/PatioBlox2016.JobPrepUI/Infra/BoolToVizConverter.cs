namespace PatioBlox2016.JobPrepUI.Infra
{
  using System;
  using System.Globalization;
  using System.Windows;
  using System.Windows.Data;

  public class BoolToVizConverter : IValueConverter
  {
    #region Implementation of IValueConverter

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool) {
        if ((bool) value) return Visibility.Visible;
        return Visibility.Collapsed;
      }

      return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}