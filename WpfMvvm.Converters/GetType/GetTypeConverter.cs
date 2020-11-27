using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает тип значения.</summary>
    /// <returns><c>value?.GetType()</c></returns>
    /// <remarks>Обратное преобразование не реализовано.</remarks>
    [ValueConversion(typeof(object), typeof(Type))]
    public class GetTypeConverter : WithoutConvertBackConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.GetType();

        /// <summary>Создаёт экземпляр <see cref="GetTypeConverter"/>.</summary>
        public GetTypeConverter() { }

        /// <summary>Экземпляр конвертера.</summary>
        public static GetTypeConverter Instance { get; } = new GetTypeConverter();

    }
}
