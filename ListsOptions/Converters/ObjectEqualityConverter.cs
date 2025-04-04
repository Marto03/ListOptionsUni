using System.Globalization;
using System.Windows.Data;

namespace ListsOptionsUI.Converters
{
    public class ObjectEqualityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Връща true ако обектите са равни
            return value != null && value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Връща null, защото не се налага двустранна конверсия
            return null;
        }
    }
}
