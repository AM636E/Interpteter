using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Calculator.Ui.Wpf.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;

            return !val ? new SolidColorBrush(Color.FromArgb(255, 180, 80, 80)) : new SolidColorBrush(Color.FromArgb(255, 50, 110, 50));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
