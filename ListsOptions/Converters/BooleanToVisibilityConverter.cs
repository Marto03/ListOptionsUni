using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ListsOptionsUI.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                bool invert = parameter != null && bool.TryParse(parameter.ToString(), out bool param) && param;
                return booleanValue ^ invert ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}
