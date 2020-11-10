using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает тип значения.</summary>
    /// <returns><c>value?.GetType()</c></returns>
    /// <remarks>Обратное преобразование не реализовано.</remarks>
    [ValueConversion(typeof(object), typeof(Type))]
    public class GetTypeConverter : IValueConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.GetType();

        /// <summary>Не реализован.</summary>
        /// <exception cref="NotImplementedException">Всегда.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>Экземпляр конвертера.</summary>
        public static GetTypeConverter Instance { get; } = new GetTypeConverter();

    }
}
