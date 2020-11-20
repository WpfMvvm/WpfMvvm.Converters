using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер возвращает результат сравнения value и parameter.</summary>
    /// <remarks>Обратное преобразование происходит только при одном из значений. Для другого присвоение значения источнику отменяется.<br/>
    /// Можно использовать для привязки RadioButton одной группы.</remarks>
    [ValueConversion(typeof(object), typeof(bool))]
    public class EqualsConverter : IValueConverter
    {
        /// <summary>Возвращает результат сравнения <paramref name="value"/> с <paramref name="parameter"/>.</summary>
        /// <param name="value">Значение для сравнения.</param>
        /// <param name="targetType">Тип целевого свойства. Используется для конвертации в целевой тип.</param>
        /// <param name="parameter">Значение для сравнения.</param>
        /// <param name="culture">Культура конвертера. Не используется.</param>
        /// <returns>Возвращается сумма (XOR) резульатат сравнения <paramref name="value"/> с <paramref name="parameter"/>
        /// и <see cref="IsNot"/> преобразованная к типу целевого свойства.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Equals(value, parameter) ^ IsNot).ConvertToType(targetType, culture);
        }

        /// <summary>Обратная конвертация <see cref="bool"/> значения.</summary>
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
        public EqualsConverter(bool isNot) => IsNot = isNot;

        /// <summary>Создаёт экземпляр <see cref="EqualsConverter"/>.</summary>
        public EqualsConverter() : this (false) { }

        /// <summary>Экземпляр конвертера.</summary>
        public static EqualsConverter Instance { get; } = new EqualsConverter();

        /// <summary>Инверсный экземпляр конвертера.</summary>
        public static EqualsConverter NotInstance { get; } = new EqualsConverter(true);
    }
}
