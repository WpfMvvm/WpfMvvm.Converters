using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер для трассировки привязок.</summary>
    /// <remarks>Отправляет в Окно Вывода сообщения при вызове методов <see cref="Convert"/> и <see cref="ConvertBack"/>.<br/>
    /// Входное значение никаким преобразованиям не подвергается.</remarks>
    [ValueConversion(typeof(object), typeof(object))]

    public class TraceConverter : IValueConverter
    {
        /// <summary>Заголовок выводимого сообщения.</summary>
        public string Title { get; set; }

        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Trace.WriteLine(Message(value, targetType, parameter, culture));
            return value;
        }

        /// <inheritdoc cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Trace.WriteLine(Message(value, targetType, parameter, culture));
            return value;
        }

        private string Message(object value, Type targetType, object parameter, CultureInfo culture, [CallerMemberName] string methodName = null)
            => $"{Title}.{methodName}({StaticMethodsOfConverters.ToString(value, culture)}, {targetType}), {StaticMethodsOfConverters.ToString(parameter, culture)}, {culture}";


        /// <summary>Создаёт экземпляр <see cref="TraceConverter"/>.</summary>
        public TraceConverter() { }

        /// <summary>Создаёт экземпляр <see cref="TraceConverter"/> с задаием значения <see cref="Title"/>.</summary>
        /// <param name="title">Значение для <see cref="Title"/>.</param>
        public TraceConverter(string title) => Title = title;


        /// <summary>Экземпляр конвертера с <see cref="Title"/>="<see cref="Instance"/>".</summary>
        public static TraceConverter Instance { get; } = new TraceConverter(nameof(Instance));
    }
}
