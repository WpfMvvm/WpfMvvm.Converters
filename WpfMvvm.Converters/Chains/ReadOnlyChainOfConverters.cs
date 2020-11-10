using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Неизменяемая цепочка конвертеров применяемая к входному значению.</summary>
    [ValueConversion(typeof(object), typeof(object))]
    [ContentProperty(nameof(Converters))]
    public partial class ReadOnlyChainOfConverters : IValueConverter
    {
        /// <summary>Неизменяемая цепочка конвертеров.</summary>
        public IReadOnlyList<IValueConverter> Converters { get; }

        /// <summary>Создаёт конвертер из последовательности конвертеров.</summary>
        /// <param name="converters">Последовательность конвертеров.</param>
        public ReadOnlyChainOfConverters(IEnumerable<IValueConverter> converters)
        {
            if (converters == null || !converters.Any(cnv => cnv != null))
                 throw new ArgumentNullException(nameof(converters), "Должен быть передан хоть один конвертер");

            Converters = converters.Where(cnv => cnv != null).ToList().AsReadOnly();
        }

        /// <summary>Создаёт конвертер из массива конвертеров.</summary>
        /// <param name="converters">Массив конвертеров или их список в параметрах.</param>
        public ReadOnlyChainOfConverters(params IValueConverter[] converters)
            : this((IEnumerable<IValueConverter>)converters)
        { }

        /// <summary>К входному значению применяется последовательно преобразование 
        /// методов <see cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/>
        /// всех конвертеров из списка <see cref="Converters"/>.</summary>
        /// <param name="value">Входное значение.</param>
        /// <param name="targetType">Тип целевого свойства.</param>
        /// <param name="parameter">Параметр конвертера.</param>
        /// <param name="culture">Культура конвертера.</param>
        /// <returns>Значение подвергнутое нескольким последовательным преобразованиям.<br/>
        /// Если один из конвертеров вернул значение <see cref="DependencyProperty.AddOwner(Type)"/>
        /// или <see cref="Binding.DoNothing"/>, то дальнейшие преобразования не применяются.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 0; i < Converters.Count; i++)
            {
                var converter = Converters[i];
                value = converter.Convert(value, targetType, parameter, culture);

                if (value == DependencyProperty.UnsetValue || value == Binding.DoNothing)
                    break;
            }
            return value;
        }

        /// <summary>К входному значению применяется последовательно преобразование в обратном порядке
        /// методов <see cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/>
        /// всех конвертеров из списка <see cref="Converters"/>.</summary>
        /// <param name="value">Входное значение для обратного преобразования.</param>
        /// <param name="targetType">Тип свойства источника.</param>
        /// <param name="parameter">Параметр конвертера.</param>
        /// <param name="culture">Культура конвертера.</param>
        /// <returns>Значение подвергнутое нескольким последовательным преобразованиям.<br/>
        /// Если один из конвертеров вернул значение <see cref="DependencyProperty.AddOwner(Type)"/>
        /// или <see cref="Binding.DoNothing"/>, то дальнейшие преобразования не применяются.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = Converters.Count - 1; i >= 0; i--)
            {
                var converter = Converters[i];
                value = converter.Convert(value, targetType, parameter, culture);

                if (value == DependencyProperty.UnsetValue || value == Binding.DoNothing)
                    break;
            }
            return value;
        }
    }

}
