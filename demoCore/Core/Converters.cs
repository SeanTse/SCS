using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace demoCore.Core
{
    public class DisplayPointConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var point = (Point)value;
            point.X = Math.Round(point.X);
            point.Y = Math.Round(point.Y);
            return point.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
