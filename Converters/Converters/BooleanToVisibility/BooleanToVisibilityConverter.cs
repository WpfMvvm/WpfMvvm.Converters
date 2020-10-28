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
    /// <see cref="Visibility.Visible"/> если <paramref name="value"/> <see langword="true"/>, иначе - <see cref="Visibility.Collapsed"/>.</returns>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    [ValueConversion(typeof(string), typeof(Visibility))]
    [ValueConversion(typeof(bool), typeof(string))]
    [ValueConversion(typeof(string), typeof(string))]
    public class BooleanToVisibilityConverter : IValueConverter
    {

        /// <summary>Прямая конвертация <see cref="bool"/> в <see cref="Visibility"/>.</summary>
        /// <param name="value">Значение для конвертации. Если оно не приводимо к <see cref="bool"/>, то возвращается <see cref="DependencyProperty.UnsetValue"/>.</param>
        /// <param name="targetType">Целевой тип</param>
        /// <param name="parameter">Не используется.</param>
        /// <param name="culture">Не используется</param>
        /// <returns><see cref="Visibility.Visible"/> если <paramref name="value"/>=<see langword="true"/>.</returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value.TryParse(out bool val))
                return (val ? Visibility.Visible : Visibility.Collapsed).ConvertToType(targetType);
            else
                return DependencyProperty.UnsetValue;
        }

        /// <summary>Обратная конвертация <see cref="Visibility"/> в <see cref="bool"/>.</summary>
        /// <param name="value">Значение для конвертации. Если оно не приводимо к <see cref="Visibility"/>, то возвращается <see cref="DependencyProperty.UnsetValue"/>.</param>
        /// <param name="targetType">Целевой тип</param>
        /// <param name="parameter">Не используется.</param>
        /// <param name="culture">Не используется</param>
        /// <returns><see langword="true"/> если <paramref name="value"/> = <see cref="Visibility.Visible"/>, иначе - <see langword="false"/>.</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value.TryParse(out Visibility val))
                return (val == Visibility.Visible).ConvertToType(targetType, culture);
            else
                return DependencyProperty.UnsetValue;
        }

        /// <summary>Экземпляр конверера.</summary>
        public static BooleanToVisibilityConverter Instance { get; } = new BooleanToVisibilityConverter();
        /// <summary>Экземпляр конвертера инверсный к <see cref="Instance"/>.</summary>
        public static ReadOnlyChainOfConverters NotInstance { get; } = new ReadOnlyChainOfConverters(BooleanNotConverter.Instance, Instance);
    }
}
