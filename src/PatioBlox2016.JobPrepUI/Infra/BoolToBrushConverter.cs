namespace PatioBlox2016.JobPrepUI.Infra
{
  using System;
  using System.Globalization;
  using System.Windows.Data;
  using System.Windows.Media;

  public class BoolToBrushConverter : IValueConverter
  {
    #region Implementation of IValueConverter

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is bool) {
        return (bool) value ? Brushes.DarkSeaGreen : Brushes.LightCoral;
      }

      return Brushes.White;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}