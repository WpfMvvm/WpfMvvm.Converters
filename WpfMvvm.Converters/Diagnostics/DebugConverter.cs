using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер для трассировки привязок.</summary>
    /// <remarks>Отправляет в Окно Вывода сообщения при вызове методов <see cref="Convert"/> и <see cref="ConvertBack"/>.<br/>
    /// Входное значение никаким преобразованиям не подвергается.</remarks>
    [ValueConversion(typeof(object), typeof(object))]

    public class DebugConverter : IValueConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"{GetType()}.{nameof(Convert)}({StaticMethodsOfConverters.ToString(value, culture)}, {targetType}), {StaticMethodsOfConverters.ToString(parameter, culture)}, {culture}");
            return value;
        }

        /// <inheritdoc cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"{GetType()}.{nameof(ConvertBack)}({StaticMethodsOfConverters.ToString(value, culture)}, {targetType}), {StaticMethodsOfConverters.ToString(parameter, culture)}, {culture}");
            return value;
        }

        /// <summary>Создаёт экземпляр <see cref="DebugConverter"/>.</summary>
        public DebugConverter() { }

        /// <summary>Экземпляр конвертера.</summary>
        public static DebugConverter Instance { get; } = new DebugConverter();
    }
}
