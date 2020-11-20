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
            //if (object.Equals(value, parameter) ^ IsNot)
            //    return true ^ IsNot;
            //return Binding.DoNothing;

            return object.Equals(value, parameter) ^ IsNot;
        }

        /// <summary>Обратная конвертация значения.</summary>
        /// <param name="value">Преобразуется в bool и потом складывается (XOR) с <see cref="IsNot"/>.</param>
        /// <param name="targetType">Тип свойства источника. Не используется.</param>
        /// <param name="parameter">Значение для возврата конвертером.</param>
        /// <param name="culture">Культура конвертера. Не используется.</param>
        /// <returns>Для <paramref name="value"/>^<see cref="IsNot"/>:<br/>
        /// <see langword="true"/> - возвращается <paramref name="parameter"/>;<br/>
        /// иначе - <see cref="Binding.DoNothing"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (StaticMethodsOfConverters.TryParse(value, out bool val) && (val ^ IsNot))
                return parameter;
            return Binding.DoNothing;
        }
        /// <summary>Если <see langword="true"/> - значение инвертируется.</summary>
        public bool IsNot { get; set; }

        /// <summary>Создаёт экземпляр <see cref="EqualsConverter"/>
        /// с заданием значения <see cref="IsNot"/>.</summary>
        /// <param name="isNot">Значение <see cref="IsNot"/>.</param>
        public EqualsTrueConverter(bool isNot) => IsNot = isNot;

        /// <summary>Создаёт экземпляр <see cref="EqualsTrueConverter"/>.</summary>
        public EqualsTrueConverter() : this(false) { }

        /// <summary>Экземпляр конвертера.</summary>
        public static EqualsTrueConverter Instance { get; } = new EqualsTrueConverter();

        /// <summary>Инверсный экземпляр конвертера.</summary>
        public static EqualsTrueConverter NotInstance { get; } = new EqualsTrueConverter(true);
    }
}
