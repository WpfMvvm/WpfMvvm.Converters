using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер преобразующий <see cref="bool"/> в <see cref="Visibility"/>.</summary>
    /// <remarks>Если приходит <see cref="string"/>, то значение конвертируется 
    /// в <see cref="bool"/> методом <see cref="bool.TryParse(string, out bool)"/>.</remarks>
    /// <returns>Если значение не <see cref="bool"/> или <see cref="string"/>, конвертируемое в <see cref="bool"/> - возвращается <see cref="DependencyProperty.UnsetValue"/>.<br/>
    /// <see cref="Visibility.Visible"/> если <paramref name="value"/> <see langword="true"/>, иначе - <see cref="Visibility.Hidden"/>.</returns>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    [ValueConversion(typeof(string), typeof(Visibility))]
    [ValueConversion(typeof(bool), typeof(string))]
    [ValueConversion(typeof(string), typeof(string))]
    public class BooleanToVisibilityHiddenConverter : IValueConverter
    {
        /// <inheritdoc cref="BooleanToVisibilityConverter.Convert"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = (Visibility)BooleanToVisibilityConverter.Instance.Convert(value, targetType, parameter, culture);
            if (result == Visibility.Collapsed)
                return Visibility.Hidden;
            return result;
        }

        /// <summary>Экземпляр конверера.</summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BooleanToVisibilityConverter.Instance.ConvertBack(value, targetType, parameter, culture);
        }
        /// <inheritdoc cref="BooleanToVisibilityConverter.Instance"/>
        public static BooleanToVisibilityHiddenConverter Instance { get; } = new BooleanToVisibilityHiddenConverter();
        /// <summary>Экземпляр конвертера инверсный к <see cref="Instance"/>.</summary>
        public static ReadOnlyChainOfConverters NotInstance { get; } = new ReadOnlyChainOfConverters(BooleanNotConverter.Instance, Instance);
    }


}
