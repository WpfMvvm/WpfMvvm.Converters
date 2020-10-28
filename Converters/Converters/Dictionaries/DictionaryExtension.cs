using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Создаёт словарь <see cref="Dictionary{TKey, TValue}"/> с заданными типами ключа и значения.</summary>
    public class DictionaryExtension : MarkupExtension
    {
        private Type _keyType = typeof(object);
        private Type _valueType = typeof(object);

        /// <summary>Тип ключа словаря.<br/>
        /// По умолчанию - <see cref="object"/>.</summary>
        /// <exception cref="ArgumentNullException">При присвоении <see langword="null"/>.</exception>
        public Type KeyType { get => _keyType; set => _keyType = value ?? throw new ArgumentNullException(nameof(value)); }

        /// <summary>Тип значения словаря.<br/>
        /// По умолчанию - <see cref="object"/>.</summary>
        /// <exception cref="ArgumentNullException">При присвоении <see langword="null"/>.</exception>
        public Type ValueType { get => _valueType; set => _valueType = value ?? throw new ArgumentNullException(nameof(value)); }

        /// <summary>Задаёт тип ключа.</summary>
        /// <param name="keyType">Тип ключа.</param>
        public DictionaryExtension(Type keyType)
        {
            KeyType = keyType ?? throw new ArgumentNullException(nameof(keyType));
        }

        /// <summary>Задаёт тип ключа и тип значения.</summary>
        /// <param name="keyType">Тип ключа.</param>
        /// <param name="valueType">Тип значения.</param>
        public DictionaryExtension(Type keyType, Type valueType) : this(keyType)
        {
            ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
        }

        /// <summary>Типы ключа и значения по умолчанию - <see cref="object"/>.</summary>
        public DictionaryExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dictionaryType = typeof(Dictionary<,>);
            var dictionaryClosedType = dictionaryType.MakeGenericType(KeyType, ValueType);

            return Activator.CreateInstance(dictionaryClosedType);
        }

    }


}
