using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает массив значений полученного <see cref="Enum"/>.</summary>
    /// <returns> <see cref="Array"/> со значениями полученного типа перечисления.
    /// Если value не тип перечисления и не значение перечисления, тогда проверяется parameter.
    /// </returns>
    [ValueConversion(typeof(Enum), typeof(Array))]

    public class EnumValuesConverter : IValueConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type typeEnum = null;

            // Проверка value на тип Enum или на значение Enum
            if (value != null)
            {
                if (value is Type type)
                    typeEnum = type;
                else
                    typeEnum = value.GetType();
            }

            // Если из value не получилось получить Enum, то проевряется parameter
            if ((typeEnum == null || !typeEnum.IsEnum) && parameter != null)
            {
                if (parameter is Type type)
                    typeEnum = type;
                else
                    typeEnum = parameter.GetType();

            }

            if (typeEnum != null && typeEnum.IsEnum)
                return Enum.GetValues(typeEnum);

            else
                return DependencyProperty.UnsetValue;

        }

        /// <summary>Не реализован.</summary>
        /// <exception cref="NotImplementedException">Всегда.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>Создаёт экземпляр <see cref="EnumValuesConverter"/>.</summary>
        public EnumValuesConverter() { }

        /// <summary>Экземпляр конвертера.</summary>
        public static EnumValuesConverter Instance { get; } = new EnumValuesConverter();
    }
}
