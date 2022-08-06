using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace VirtualKeyboardSample.Converters
{
    public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value is bool b && b
                ? parameter == null 
                    ? Visibility.Visible 
                    : Visibility.Collapsed
                : parameter == null
                    ? Visibility.Collapsed
                    : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
