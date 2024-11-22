using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace MauiApp1.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        // Optional: Allow customization of the format via a parameter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is TimeSpan timeSpan)
            {
                string format = "hh\\:mm tt"; // Default to 12-hour format with AM/PM
                if (parameter is string customFormat)
                {
                    format = customFormat;
                }
                return timeSpan.ToString(format);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string str && TimeSpan.TryParse(str, out TimeSpan timeSpan))
            {
                return timeSpan;
            }
            return value;
        }
    }
}
