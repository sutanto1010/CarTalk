using System;
using System.Globalization;
using Xamarin.Forms;

namespace CarTalk.Converter
{
    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            return !(bool) value;
        }
    }
}
