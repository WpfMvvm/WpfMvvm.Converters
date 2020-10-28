using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер-обёртка над internal <see href="https://referencesource.microsoft.com/PresentationFramework/R/a224c73beb6d4d79.html">DefaultValueConverter</see>.</summary>
    /// <remarks>В конверетере можно явно задать типы целевого свойства и свойства источника: <see cref="TargetType"/> и <see cref="SourceType"/>.<br/>
    /// Если какой-то из них не задан, то используется параметр target из методов <see cref="Convert(object, Type, object, CultureInfo)"/>
    /// и <see cref="ConvertBack(object, Type, object, CultureInfo)"/>.<para/>
    /// В конвертере добавлена обработка исключений возникающих в <see href="https://referencesource.microsoft.com/PresentationFramework/R/a224c73beb6d4d79.html">DefaultValueConverter</see>.<br/>
    /// Если исключение возникло в методе <see cref="IValueConverter.Convert(object, Type, object, CultureInfo)"/> - возвращается <see cref="DependencyProperty.UnsetValue"/>.<br/>
    /// Если в <see cref="IValueConverter.ConvertBack(object, Type, object, CultureInfo)"/> - возвращается <see cref="Binding.DoNothing"/>.</remarks>
    public class ValueTypeConverter : IValueConverter
    {
        /// <summary>Тип свойства источника. Если не задан, то используется targetType из <see cref="ConvertBack(object, Type, object, CultureInfo)"/>.</summary>
        public Type SourceType { get;}

        /// <summary>Тип целевого свойства. Если не задан, то используется targetType из <see cref="Convert(object, Type, object, CultureInfo)"/>.</summary>
        public Type TargetType { get;}

        /// <summary>Создаёт экземпляр конвертера с заданными типами.</summary>
        /// <param name="sourceType">Значение для <see cref="SourceType"/>.</param>
        /// <param name="targetType">Значение для <see cref="TargetType"/>.</param>
        public ValueTypeConverter(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (TargetType != null)
                targetType = TargetType;

            var defaultConverter = StaticMethodsOfConverters.GetDefaultValueConverter(value.GetType(), targetType, false);

            try
            {
                return defaultConverter.Convert(value, targetType, parameter, culture);
            }
            catch (Exception)
            {

                return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            Type sourceType = SourceType ?? targetType;

            var defaultConverter = StaticMethodsOfConverters.GetDefaultValueConverter(sourceType, value.GetType(), true);

            try
            {
                return defaultConverter.ConvertBack(value, sourceType, parameter, culture);
            }
            catch (Exception)
            {

                return Binding.DoNothing;
            }
        }

        /// <summary>Возвращает internal <see href="https://referencesource.microsoft.com/PresentationFramework/R/a224c73beb6d4d79.html">DefaultValueConverter</see>
        /// в оболочке с обрабокой исключений.</summary>
        public static ValueTypeConverter InstanceWithExceptionHandling { get; }
            = new ValueTypeConverter(null, null);
    }
}
