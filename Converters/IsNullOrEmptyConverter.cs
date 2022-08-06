using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace VirtualKeyboardSample.Converters
{
    public class IsNullOrEmptyConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var str = value as string;
            return (bool)string.IsNullOrEmpty(str);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
