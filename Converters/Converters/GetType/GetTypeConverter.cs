using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает тип значения.</summary>
    /// <returns><c>value?.GetType()</c></returns>
    public class GetTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value?.GetType();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>Экземпляр конвертера.</summary>
        public static GetTypeConverter Instance { get; } = new GetTypeConverter();

    }
}
