using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    public static partial class StaticMethodsOfConverters
    {
        /// <summary>Получает экземпляр <see cref="ValueTypeConverter"/>,
        /// используемого для преобразования значения между исходным и целевым типами.</summary>
        /// <param name="sourceType">Тип свойства источника.</param>
        /// <param name="targetType">Тип целевого свойства.</param>
        /// <returns>Экземпляр из приватного словаря <see cref="ValueTypeConverters"/>.<br/>
        /// Если там нет экземпляра для указанных параметров, то он создаётся и добавляется в словарь.</returns>
        /// <remarks>Метод не потокозащищённый. Подразумевается его использование из потока Диспетчера.</remarks>
        public static ValueTypeConverter GetValueTypeConverter(Type sourceType, Type targetType)
        {
            if (!ValueTypeConverters.TryGetValue((sourceType, targetType), out ValueTypeConverter converter))
            {
                // Создание экземпляра и его добавление в словарь.
                converter = new ValueTypeConverter(sourceType, targetType);
                ValueTypeConverters.Add((sourceType, targetType), converter);
            }
            return converter;
        }

        /// <summary>Словарь ранее созданных экземпляров конвертера <see cref="ValueTypeConverter"/>.</summary>
        private static readonly Dictionary<(Type sourceType, Type targetType), ValueTypeConverter> ValueTypeConverters
                = new Dictionary<(Type sourceType, Type targetType), ValueTypeConverter>()
                {
                    {
                        (ValueTypeConverter.InstanceWithExceptionHandling.SourceType, ValueTypeConverter.InstanceWithExceptionHandling.TargetType),
                        ValueTypeConverter.InstanceWithExceptionHandling
                    } 
                };

    }
}
