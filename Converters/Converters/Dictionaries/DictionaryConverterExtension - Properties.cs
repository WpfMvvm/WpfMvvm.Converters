using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace WpfMvvm.Converters
{
    // Свойства
    [ContentProperty(nameof(Dictionary))]
    [DefaultBindingProperty(nameof(Binding))]
    public partial class DictionaryConverterExtension : MarkupExtension
    {
        private Type _valueType = typeof(object);
        private Type _keyType = typeof(object);

        /// <summary>Привязка для словаря.</summary>
        public Binding Binding { get; set; }

        /// <summary>Словарь для заполнения в XAML.</summary>
        public IDictionary Dictionary { get; set; } = new Dictionary<object, object>();

        /// <summary>Тип ключа словаря.<br/>
        /// Если <see langword="null"/> - используется значение по умолчанию.<br/>
        /// По умолчанию - <see cref="object"/>.</summary>
        [DefaultValue(typeof(object))]
        public Type KeyType
        {
            get => _keyType;
            set
            {
                if (value == null)
                    value = typeof(object);

                if (_keyType == value)
                    return;

                _keyType = value;
                ChangeTypeDictionary();
            }
        }

        /// <summary>Тип значения словаря.<br/>
        /// Если <see langword="null"/> - используется значение по умолчанию.<br/>
        /// По умолчанию - <see cref="object"/>.</summary>
        [DefaultValue(typeof(object))]
        public Type ValueType
        {
            get => _valueType;
            set
            {
                if (value == null)
                    value = typeof(object);

                if (_valueType == value)
                    return;

                _valueType = value;
                ChangeTypeDictionary();
            }
        }


        /// <summary>Определяет какой конвертер бyдет возвращён.</summary>
        /// <value><see langword="null"/> - <see cref="DictionaryConverter"/>,<br/>
        /// <see langword="true"/> - <see cref="DictionaryTypeConverter"/> с <see cref="DictionaryTypeConverter.UseBasicTypes"/>=<see langword="true"/>,<br/>
        /// <see langword="false"/> - <see cref="DictionaryTypeConverter"/> с <see cref="DictionaryTypeConverter.UseBasicTypes"/>=<see langword="false"/>.</value>
        [DefaultValue(null)]
        public bool? UseBasicTypes { get; set; }


        /// <summary>Изменение типа словаря <see cref="Dictionary"/>.</summary>
        private void ChangeTypeDictionary()
        {
            var dictionaryType = typeof(Dictionary<,>);
            var dictionaryClosedType = dictionaryType.MakeGenericType(KeyType ?? typeof(object), ValueType ?? typeof(object));

            Dictionary = (IDictionary)Activator.CreateInstance(dictionaryClosedType);
        }
    }


}
