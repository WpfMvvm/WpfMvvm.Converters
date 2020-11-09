using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    /// <summary>Конвертер получает тип value и по нему возвращает значение из словаря.<br/>
    ///  Свойство <see cref="UseBasicTypes"/> задаёт возможность использования базовых типов.</summary>
    /// <remarks> В классе переопределён метод базового класса <see cref="DictionaryConverter.GetValue(IDictionary, object)"/> на поиск по типу полученного ключа. <br/>
    /// Удобно использовать как селектор шаблонов или стилей.</remarks>
    [ValueConversion(typeof(Type), typeof(object))]
    public class DictionaryTypeConverter : DictionaryConverter
    {
        /// <summary>Если <see langword="false"/>, то ищется только ключ полностью совпадающий с заданным типом.<br/>
        /// Если <see langword="true"/>, то также используются базовые типы. 
        /// Если их несколько, то выбирается ближайший предок.</summary>
        public bool UseBasicTypes
        {
            get { return (bool)GetValue(UseBasicTypesProperty); }
            set { SetValue(UseBasicTypesProperty, value); }
        }

        /// <summary>Using a DependencyProperty as the backing store for UseBasicTypes.  This enables animation, styling, binding, etc...</summary>
        public static readonly DependencyProperty UseBasicTypesProperty =
            DependencyProperty.Register(nameof(UseBasicTypes), typeof(bool), typeof(DictionaryTypeConverter), new PropertyMetadata(true));

        /// <summary>Инициализирует новый экземпляр конвертера <see cref="DictionaryTypeConverter"/>.<br/>
        /// В <see cref="Dictionary"/> записывается новый экземпляр <c>new Dictionary&lt;Type, object&gt;()</c>.</summary>
        public DictionaryTypeConverter()
            : this(new Dictionary<Type, object>())
        { }

        /// <summary>Инициализирует новый экземпляр конвертера <see cref="DictionaryTypeConverter"/> переданным словарём.</summary>
        /// <param name="dictionary">Cловарь записываемый в <see cref="Dictionary"/>.</param>
        public DictionaryTypeConverter(IDictionary dictionary)
            => Dictionary = dictionary;

        protected override object GetValue(IDictionary dictionary, object key)
        {
            Type keyType;
            if (key is Type type)
                keyType = type;
            else
                keyType = key.GetType();

            if (!dictionary.Contains(keyType))
            {
                if (!UseBasicTypes)
                    return null;
                else
                {
                    keyType = typeof(object);
                    foreach (Type tp in dictionary.Keys.OfType<Type>().Where(t => t.IsAssignableFrom(keyType)))
                    {
                        if (keyType.IsAssignableFrom(tp))
                            keyType = tp;
                    }
                }

            }

            return base.GetValue(dictionary, keyType);
        }
        protected override Freezable CreateInstanceCore()
            => new DictionaryTypeConverter();

        /// <summary>Экземпляр конвертера с <see cref="UseBasicTypes"/>=<see langword="true"/>.<br/>
        /// Экземпляр заморожен.<br/>
        /// Свойствам заданы значения и они не изменяемы: <see cref="Dictionary"/>=<see langword="null"/>, <see cref="UseBasicTypes"/>=<see langword="true"/>.<br/>
        /// Словарь должен передаваться через parameters метода <see cref="Convert(object, Type, object, CultureInfo)"/>.</summary>
        public static DictionaryTypeConverter InstanceUseBaseTypes { get; }

        /// <summary>Экземпляр конвертера с <see cref="UseBasicTypes"/>=<see langword="false"/>.<br/>
        /// Экземпляр заморожен.<br/>
        /// Свойствам заданы значения и они не изменяемы: <see cref="Dictionary"/>=<see langword="null"/>, <see cref="UseBasicTypes"/>=<see langword="false"/>.<br/>
        /// Словарь должен передаваться через parameters метода <see cref="Convert(object, Type, object, CultureInfo)"/>.</summary>
        public static DictionaryTypeConverter InstanceNotUseBaseTypes { get; }

        /// <summary>Записывает в <see cref="InstanceUseBaseTypes"/> и <see cref="InstanceNotUseBaseTypes"/> статические замороженные экземпляры конвертеров.</summary>
        static DictionaryTypeConverter()
        {
            InstanceUseBaseTypes = new DictionaryTypeConverter(null) { UseBasicTypes = true };
            InstanceUseBaseTypes.Freeze();

            InstanceNotUseBaseTypes = new DictionaryTypeConverter(null) { UseBasicTypes = false };
            InstanceNotUseBaseTypes.Freeze();
        }

    }

}
