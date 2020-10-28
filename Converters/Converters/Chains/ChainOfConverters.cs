using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Цепочка конвертеров применяемая к входному значению.</summary>
    [ValueConversion(typeof(object), typeof(object))]
    [ContentProperty(nameof(Converters))]
    public class ChainOfConverters : IValueConverter
    {
        /// <summary>Цепочка конвертеров.</summary>
        public List<IValueConverter> Converters { get; set; } = new List<IValueConverter>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Converters != null)
                for (int i = 0; i < Converters.Count; i++)
                {
                    var converter = Converters[i];
                    value = converter.Convert(value, targetType, parameter, culture);

                    if (value == DependencyProperty.UnsetValue || value == Binding.DoNothing)
                        break;
                }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Converters != null)
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
