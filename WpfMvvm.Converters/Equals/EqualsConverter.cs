using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер возвращает результат сравнения value и parameter.</summary>
    /// <remarks>Обратное преобразование не реализовано.</remarks>
    [ValueConversion(typeof(object), typeof(bool))]
    public class EqualsConverter : IValueConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return object.Equals(value, parameter);
        }

        /// <summary>Не реализован.</summary>
        /// <exception cref="NotImplementedException">Всегда.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>Экземпляр конвертера.</summary>
        public static EqualsConverter Instance { get; } = new EqualsConverter();

        /// <summary>Инверсный экземпляр конвертера.</summary>
        public static ReadOnlyChainOfConverters NotInstance { get; } = new ReadOnlyChainOfConverters(BooleanNotConverter.Instance, Instance);
    }
}
