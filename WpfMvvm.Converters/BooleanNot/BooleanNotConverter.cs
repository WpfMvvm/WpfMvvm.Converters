using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Инвертирует полученное логическое значение.</summary>
    /// <remarks>Если приходит <see cref="string"/>, то значение конвертируется 
    /// в <see cref="bool"/> методом <see cref="bool.TryParse(string, out bool)"/>.</remarks>
    /// <returns>Если значение не <see cref="bool"/> или <see cref="string"/>, конвертируемое в <see cref="bool"/> - возвращается <see cref="DependencyProperty.UnsetValue"/>.</returns>
    [ValueConversion(typeof(bool), typeof(bool))]
    [ValueConversion(typeof(string), typeof(bool))]
    [ValueConversion(typeof(bool), typeof(string))]
    [ValueConversion(typeof(string), typeof(string))]
    public class BooleanNotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value.TryParse(out bool valBool))
                return (!valBool).ConvertToType(targetType, culture);

            return DependencyProperty.UnsetValue;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.TryParse(out bool valBool))
                return (!valBool).ConvertToType(targetType, culture);

            return DependencyProperty.UnsetValue;
        }

        /// <summary>Экземпляр конвертера.</summary>
        public static BooleanNotConverter Instance { get; } = new BooleanNotConverter();
    }
}
