using System.Globalization;
using System.Windows.Data;

namespace ListsOptionsUI.Converters
{
    public class HotelToIsCheckedMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var selectedHotel = values[0];
            var currentHotel = values[1];

            return Equals(selectedHotel, currentHotel);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // Не е нужно тук
            return Binding.DoNothing as object[];
        }
    }
}
