using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер для трассировки привязок.</summary>
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"{GetType()}.{nameof(Convert)}({StaticMethodsOfConverters.ToString(value, culture)}, {targetType}), {StaticMethodsOfConverters.ToString(parameter, culture)}, {culture}");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine($"{GetType()}.{nameof(ConvertBack)}({StaticMethodsOfConverters.ToString(value, culture)}, {targetType}), {StaticMethodsOfConverters.ToString(parameter, culture)}, {culture}");
            return value;
        }

        /// <summary>Экземпляр конвертера.</summary>
        public static DebugConverter Instance { get; } = new DebugConverter();
    }
}
