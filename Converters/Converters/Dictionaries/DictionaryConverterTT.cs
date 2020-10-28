using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    public abstract class BaseDictionaryConverter : Freezable, IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>Возвращает <see cref="DictionaryConverter{TKey, TValue}"/> типизированный заданными типами.</summary>
        /// <param name="keyType">Тип ключа <see cref="DictionaryConverter{TKey, TValue}"/>.</param>
        /// <param name="valueType">Тип элементов <see cref="DictionaryConverter{TKey, TValue}"/>.</param>
        /// <returns>Возвращает новый экземпляр <see cref="DictionaryConverter{TKey, TValue}"/>.</returns>
        public BaseDictionaryConverter CreateInstance(Type keyType, Type valueType)
        {
            if (keyType == null)
                throw new ArgumentNullException(nameof(keyType));
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType));

            if (Converters.TryGetValue((keyType, valueType), out var converter))
                return (BaseDictionaryConverter)converter.CreateInstanceCore();

            var converterType = typeof(DictionaryConverter<,>);
            var converterCloseType = converterType.MakeGenericType(keyType, valueType);

            return (BaseDictionaryConverter)Activator.CreateInstance(converterCloseType);
        }

        /// <summary>Словарь экземпляров конвертеров для каждого использованного сочетания типов ключа и данных.</summary>
        /// <remarks>При первом обращении к <see cref="DictionaryConverter{TKey, TValue}"/> с указаннием конкретных типов,
        /// в словарь по ключу типа записываеся экземпляр из свойства <see cref="DictionaryConverter{TKey, TValue}.Instance"/>.</remarks>
        protected static readonly Dictionary<(Type, Type), BaseDictionaryConverter> Converters
            = new Dictionary<(Type, Type), BaseDictionaryConverter>();

    }

    /// <summary>Конвертер преобразующий ключ в значение по словарю.</summary>
    /// <typeparam name="TKey">Тип ключа словаря.</typeparam>
    /// <typeparam name="TValue">Тип значения словаря.</typeparam>
    [ContentProperty(nameof(Dictionary))]
    public class DictionaryConverter<TKey, TValue> : BaseDictionaryConverter
    {
        /// <summary>Словарь для поиска значений.<br/>
        /// Используется в случае когда в parameter нет словаря.</summary>
        public IDictionary<TKey, TValue> Dictionary
        {
            get { return (IDictionary<TKey, TValue>)GetValue(DictionaryProperty); }
            set { SetValue(DictionaryProperty, value); }
        }

        /// <summary>Using a DependencyProperty as the backing store for Dictionary.  This enables animation, styling, binding, etc...</summary>
        public static readonly DependencyProperty DictionaryProperty =
            DependencyProperty.Register(nameof(Dictionary), typeof(IDictionary<TKey, TValue>), typeof(DictionaryConverter<TKey, TValue>), new PropertyMetadata(null));

        /// <summary>Инициализирует новый экземпляр конвертера <see cref="DictionaryConverter{TKey, TValue}"/>.<br/>
        /// В <see cref="Dictionary"/> записывается новый экземпляр <c>new Dictionary&lt;Type, object&gt;()</c>.</summary>
        public DictionaryConverter()
            : this(new Dictionary<TKey, TValue>())
        { }

        /// <summary>Инициализирует новый экземпляр конвертера <see cref="DictionaryConverter{TKey, TValue}"/> переданным словарём.</summary>
        /// <param name="dictionary">Cловарь записываемый в <see cref="Dictionary"/>.</param>
        public DictionaryConverter(IDictionary<TKey, TValue> dictionary)
            => Dictionary = dictionary;

        /// <summary>Возвращает значение из словаря по заданному ключу.</summary>
        /// <param name="value">Ключ. Если не приводится к <see cref="TKey"/> - возвращается <see cref="DependencyProperty.UnsetValue"/>.</param>
        /// <param name="targetType">Не используется.</param>
        /// <param name="parameter">Если содержит словарь, то поиск производится по нему.
        /// Иначе используется словарь из <see cref="Dictionary"/>.</param>
        /// <param name="culture">Не используется.</param>
        /// <returns>Найденное значение ключа или <see cref="DependencyProperty.UnsetValue"/>.<br/>
        /// Если ни в <paramref name="parameter"/>, ни в <see cref="Dictionary"/> нет словаря - возвращается <see cref="DependencyProperty.UnsetValue"/>.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TKey key)
            {
                if (!(parameter is IDictionary<TKey, TValue> dictionary))
                    dictionary = Dictionary;

                if (dictionary != null && dictionary.TryGetValue(key, out var val))
                    return val;
            }
            return DependencyProperty.UnsetValue;
        }

        /// <summary>Не реализован.</summary>
        /// <returns>Всегда исключение <see cref="NotImplementedException"/>.</returns>
        /// <exception cref="NotImplementedException">Всегда.</exception>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected override Freezable CreateInstanceCore()
            => new DictionaryConverter<TKey, TValue>();

        /// <summary>Экземпляр конвертера.<br/>
        /// Экземпляр заморожен: свойство <see cref="Dictionary"/>=<see langword="null"/> и неизменяемо.<br/>
        /// Словарь должен передаваться через parameters метода <see cref="Convert(object, Type, object, CultureInfo)"/>.</summary>
        public static DictionaryConverter<TKey, TValue> Instance { get; }

        /// <summary>Записывает в <see cref="Instance"/> и в <see cref="BaseDictionaryConverter.Converters"/> статический замороженный экземпляр конвертера.</summary>
        static DictionaryConverter()
        {
            Instance = new DictionaryConverter<TKey, TValue>(null);
            Instance.Freeze();

            Converters.Add((typeof(TKey), typeof(TValue)), Instance);
        }

    }

}
