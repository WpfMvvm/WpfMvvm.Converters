using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    public static partial class StaticMethodsOfConverters
    {
        /// <summary>Получает экземпляр системного
        /// internal <see href="https://referencesource.microsoft.com/PresentationFramework/R/a224c73beb6d4d79.html">DefaultValueConverter</see>,
        /// используемого по умолчанию для преобразования значения между исходным и целевым типами.</summary>
        /// <param name="sourceType">Тип свойства источника.</param>
        /// <param name="targetType">Тип целевого свойства.</param>
        /// <param name="targetToSource">Конвертер с реализованным методом <see cref="IValueConverter.ConvertBack(object, Type, object, System.Globalization.CultureInfo)"/>
        /// для обратного преобразования.</param>
        /// <returns>Экземпляр из приватного словаря <see cref="DefaultValueConverters"/>.<br/>
        /// Если там нет экземпляра для указанных параметров, то он создаётся методом <see cref="CreateDefaultValueConverter"/> и добавляется в словарь.</returns>
        /// <remarks>Метод не потокозащищённый. Подразумевается его использование из потока Диспетчера.<para/>
        /// При использовании полученного конвертера учитывайте, что если значение нельзя преобразовать в требуемый тип,
        /// то в <see href="https://referencesource.microsoft.com/PresentationFramework/R/a224c73beb6d4d79.html">DefaultValueConverter</see>
        /// выбрасывается исключение.</remarks>
        public static IValueConverter GetDefaultValueConverter(Type sourceType, Type targetType, bool targetToSource)
        {
            if (!DefaultValueConverters.TryGetValue((sourceType, targetType, targetToSource), out IValueConverter converter))
            {
                // Массив параметров метода.
                object[] convertParams = { sourceType, targetType, targetToSource, DataBindEngine };

                // Создание экземпляра и его добавление в словарь.
                converter = (IValueConverter)CreateDefaultValueConverter.Invoke(null, convertParams);
                DefaultValueConverters.Add((sourceType, targetType, targetToSource), converter);
            }
            return converter;
        }

        /// <summary>Словарь ранее созданных экземпляров конвертеров.</summary>
        private static readonly Dictionary<(Type sourceType, Type targetType, bool targetToSource), IValueConverter> DefaultValueConverters
                = new Dictionary<(Type sourceType, Type targetType, bool targetToSource), IValueConverter>();

        /// <summary>Экземпляр internal класса <see href="https://referencesource.microsoft.com/PresentationFramework/R/327d897d35cc90ed.html">DataBindEngine</see>
        /// из свойства <see href="https://referencesource.microsoft.com/PresentationFramework/R/67d798762acb5730.html">DataBindEngine.CurrentDataBindEngine</see>.</summary>
        private static readonly object DataBindEngine;

        /// <summary>internal метод <see href="https://referencesource.microsoft.com/PresentationFramework/R/c72fce88c319145f.html">DefaultValueConverter.Create</see>.</summary>
        /// <remarks>static constructor - returns a ValueConverter suitable for converting between
        /// the source and target.  The flag indicates whether targetToSource
        /// conversions are actually needed.
        /// if no Converter is needed, return DefaultValueConverter.ValueConverterNotNeeded marker.
        /// if unable to create a DefaultValueConverter, return null to indicate error.</remarks>
        private static readonly MethodInfo CreateDefaultValueConverter;

        /// <summary>Инициализирует статические свойства и поля.</summary>
        static StaticMethodsOfConverters()
        {
            // Статическая часть - выполняется однократно.

            // Получение сборки
            var ass = typeof(Microsoft.Win32.CommonDialog).Assembly;

            // Получение Type internal типов.
            var typeDefaultValueConverter = ass.GetType("MS.Internal.Data.DefaultValueConverter");
            var typeDataBindEngine = ass.GetType("MS.Internal.Data.DataBindEngine");

            // Получение экземпляра DataBindEngine.
            var currentDataBindEngineProperty = typeDataBindEngine
                .GetProperty("CurrentDataBindEngine", BindingFlags.NonPublic | BindingFlags.Static);
            DataBindEngine = currentDataBindEngineProperty.GetValue(null);

            // Получение MethodInfo метода создающего дефолтный конвертер.
            CreateDefaultValueConverter = typeDefaultValueConverter.GetMethod("Create", BindingFlags.NonPublic | BindingFlags.Static);
        }
    }
}
