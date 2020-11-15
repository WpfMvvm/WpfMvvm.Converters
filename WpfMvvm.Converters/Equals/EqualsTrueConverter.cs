using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{

    /// <summary>Конвертер сравнивает значение и параметр. Если они не равны, то возвращается <see cref="Binding.DoNothing"/>.</summary>
    /// <remarks>Можно использовать для привязки RadioButton одной группы.</remarks>
    [ValueConversion(typeof(object), typeof(bool))]
    public class EqualsTrueConverter : IValueConverter
    {
        /// <inheritdoc cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (object.Equals(value, parameter))
                return true;
            return Binding.DoNothing;
        }

        /// <inheritdoc cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (object.Equals(value, true))
                return parameter;
            return Binding.DoNothing;
        }

        /// <summary>Создаёт экземпляр <see cref="EqualsTrueConverter"/>.</summary>
        public EqualsTrueConverter() { }

        /// <summary>Экземпляр конвертера.</summary>
        public static EqualsTrueConverter Instance { get; } = new EqualsTrueConverter();

        /// <summary>Инверсный экземпляр конвертера.</summary>
        public static ReadOnlyChainOfConverters NotInstance { get; } = new ReadOnlyChainOfConverters(BooleanNotConverter.Instance, Instance);
    }
}
