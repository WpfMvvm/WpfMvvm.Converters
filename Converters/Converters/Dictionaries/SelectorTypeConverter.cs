using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{

    /// <summary>Базовый класс объявлен для удобства использования коллекции конвертеров.</summary>
    public abstract class BaseSelectorTypeConverter : Freezable, IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);


        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>Возвращает <see cref="SelectorTypeConverter{TValue}"/> типизированный заданным типом.</summary>
        /// <param name="valueType">Тип элементов <see cref="SelectorTypeConverter{TValue}"/>.</param>
        /// <returns>Возвращает новый экземпляр <see cref="SelectorTypeConverter{TValue}"/>.</returns>
        public BaseSelectorTypeConverter CreateInstance(Type valueType)
        {
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType));

            if (Converters.TryGetValue(valueType, out var converter))
                return (BaseSelectorTypeConverter) converter.CreateInstanceCore();

            var converterType = typeof(SelectorTypeConverter<>);
            var converterCloseType = converterType.MakeGenericType(valueType);

            return (BaseSelectorTypeConverter)Activator.CreateInstance(converterCloseType);
        }

        /// <summary>Словарь экземпляров конвертеров для каждого использованного типа данных.</summary>
        /// <remarks>При первом обращении к <see cref="SelectorTypeConverter{TValue}"/> с указаннием конкретного типа,
        /// в словарь по ключу типа записываеся экземпляр из свойства <see cref="SelectorTypeConverter{TValue}.Instance"/>.</remarks>
        protected static readonly Dictionary<Type, BaseSelectorTypeConverter> Converters
            = new Dictionary<Type, BaseSelectorTypeConverter>();

    }

    /// <summary>Конвертер - Селектор по типу полученого значения.</summary>
    /// <returns><see cref="TValue"/> полученного по ключу.</returns>
    [ContentProperty(nameof(Dictionary))]
    public class SelectorTypeConverter<TValue> : BaseSelectorTypeConverter
    {
        /// <summary>Словарь для поиска значений.<br/>
        /// Используется в случае когда в parameter нет словаря.
        /// </summary>
        public IDictionary<Type, TValue> Dictionary
        {
            get { return (IDictionary<Type, TValue>)GetValue(DictionaryProperty); }
            set { SetValue(DictionaryProperty, value); }
        }

        /// <summary>Using a DependencyProperty as the backing store for Dictionary.  This enables animation, styling, binding, etc...</summary>
        public static readonly DependencyProperty DictionaryProperty =
            DependencyProperty.Register(nameof(Dictionary), typeof(IDictionary<Type, TValue>), typeof(SelectorTypeConverter<TValue>), new PropertyMetadata(null));



        /// <summary>Если <see langword="false"/>, то ищется только ключ полностью совпадающий с заданным типом.<br/>
        /// Если <see langword="true"/>, то базовые типы тоже используются. 
        /// Если их несколько, то выбирается ближайший.</summary>
        public bool UseBasicTypes
        {
            get { return (bool)GetValue(UseBasicTypesProperty); }
            set { SetValue(UseBasicTypesProperty, value); }
        }

        /// <summary>Using a DependencyProperty as the backing store for UseBasicTypes.  This enables animation, styling, binding, etc...</summary>
        public static readonly DependencyProperty UseBasicTypesProperty =
            DependencyProperty.Register(nameof(UseBasicTypes), typeof(bool), typeof(SelectorTypeConverter<TValue>), new PropertyMetadata(true));

        /// <summary>Инициализирует новый экземпляр конвертера <see cref="SelectorForValueTypeConverter"/>.<br/>
        /// В <see cref="Dictionary"/> записывается новый экземпляр <c>new Dictionary&lt;Type, TValue&gt;()</c>.</summary>
        public SelectorTypeConverter()
            : this(new Dictionary<Type, TValue>())
        { }

        /// <summary>Инициализирует новый экземпляр конвертера <see cref="SelectorForValueTypeConverter"/> переданным словарём.</summary>
        /// <param name="dictionary">Cловарь записываемый в <see cref="Dictionary"/>.</param>
        public SelectorTypeConverter(IDictionary<Type, TValue> dictionary)
            => Dictionary = dictionary;


        /// <summary>Возвращает значение из словаря по типу ключа.</summary>
        /// <param name="value">Ключ. Если это не <see cref="Type"/>, то используется value.GetType().</param>
        /// <param name="targetType">Не используется.</param>
        /// <param name="parameter">Если содержит словарь, то поиск производится по нему.
        /// Иначе используется словарь из <see cref="Dictionary"/>.</param>
        /// <param name="culture">Не используется.</param>
        /// <returns>Найденное значение <see cref="TValue"/> ключа или <see cref="DependencyProperty.UnsetValue"/>.<br/>
        /// Если ни в <paramref name="parameter"/>, ни в <see cref="Dictionary"/> нет словаря - возвращается <see cref="DependencyProperty.UnsetValue"/>.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Type valueType;
                if (value is Type type)
                    valueType = type;
                else
                    valueType = value.GetType();

                if (!(parameter is IDictionary<Type, TValue> dictionary))
                    dictionary = Dictionary;

                if (dictionary != null && dictionary.TryGetValue(valueType, out TValue template))
                    return template;

                if (UseBasicTypes)
                {
                    Type baseType = typeof(object);
                    foreach (Type tp in dictionary.Keys.OfType<Type>().Where(t => t.IsAssignableFrom(valueType)))
                    {
                        if (baseType.IsAssignableFrom(tp))
                            baseType = tp;
                    }

                    if (baseType != typeof(object))
                        return dictionary[baseType];

                    if (dictionary.TryGetValue(typeof(object), out TValue defaultTemplate))
                        return defaultTemplate;
                }

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
            => new SelectorTypeConverter<TValue>();

        /// <summary>Экземпляр конвертера с <see cref="UseBasicTypes"/>=<see langword="true"/>.<br/>
        /// Экземпляр заморожен.<br/>
        /// Свойствам заданы значения и они не изменяемы: <see cref="Dictionary"/>=<see langword="null"/>, <see cref="UseBasicTypes"/>=<see langword="true"/>.<br/>
        /// Словарь должен передаваться через parameters метода <see cref="Convert(object, Type, object, CultureInfo)"/>.</summary>
        public static SelectorTypeConverter<TValue> Instance { get; }

        /// <summary>Экземпляр конвертера с <see cref="UseBasicTypes"/>=<see langword="false"/>.<br/>
        /// Экземпляр заморожен.<br/>
        /// Свойствам заданы значения и они не изменяемы: <see cref="Dictionary"/>=<see langword="null"/>, <see cref="UseBasicTypes"/>=<see langword="false"/>.<br/>
        /// Словарь должен передаваться через parameters метода <see cref="Convert(object, Type, object, CultureInfo)"/>.</summary>
        public static SelectorTypeConverter<TValue> InstanceNotUseBaseTypes { get; }

        /// <summary>Записывает в <see cref="Instance"/> и <see cref="InstanceNotUseBaseTypes"/> статические замороженные экземпляры конвертеров.<br/>
        /// Экземпляр <see cref="Instance"/> добавляется в <see cref="BaseSelectorTypeConverter.Converters"/>.</summary>
        static SelectorTypeConverter()
        {
            Instance = new SelectorTypeConverter<TValue>(null) { UseBasicTypes = true };
            Instance.Freeze();

            InstanceNotUseBaseTypes = new SelectorTypeConverter<TValue>(null) { UseBasicTypes = false };
            InstanceNotUseBaseTypes.Freeze();

            Converters.Add(typeof(TValue), Instance);
        }

    }

}
