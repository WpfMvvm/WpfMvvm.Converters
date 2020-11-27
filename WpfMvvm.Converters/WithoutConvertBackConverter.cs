using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Базовый абстрактный класс для реализации ковертеров без обратного (от цели к источнику) преобразования.<para/>
    /// При обращении к методу <see cref="ConvertBack(object, Type, object, CultureInfo)"/> всегда выкидывается
    /// исключение <see cref="NotImplementedConvertBackException"/>.</summary>
    public abstract class WithoutConvertBackConverter : IValueConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>Не реализован.</summary>
        /// <returns>Всегда исключение <see cref="NotImplementedConvertBackException"/>.</returns>
        /// <exception cref="NotImplementedException">Всегда <see cref="NotImplementedConvertBackException"/>.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw NotImplementedConvertBackException;

        /// <summary>Ошибка при обращении к методу <see cref="ConvertBack(object, Type, object, CultureInfo)"/>.</summary>
        public static NotImplementedException NotImplementedConvertBackException { get; }
            = new NotImplementedException($"Метод {nameof(ConvertBack)} не реализован.");
    }
}
