﻿using System;

namespace WpfMvvm.Converters
{
    // Конструкторы
    public partial class DictionaryConverterExtension 
    {
        /// <summary>Задаёт свойство <see cref="UseTypes"/>.</summary>
        /// <param name="useBaseTypes">Значение для свойства <see cref="UseTypes"/>.</param>
        public DictionaryConverterExtension(UseTypesEnum useBaseTypes)
            //: this()
            => UseTypes = useBaseTypes;

        ///// <summary>Задаёт тип ключа и тип значения.</summary>
        ///// <param name="keyType">Тип ключа.</param>
        ///// <param name="valueType">Тип значения.</param>
        //public DictionaryConverterExtension(Type keyType, Type valueType)
        //    : this()
        //{
        //    KeyType = keyType ?? throw new ArgumentNullException(nameof(keyType));
        //    ValueType = valueType ?? throw new ArgumentNullException(nameof(valueType));
        //}

        ///// <summary>Задаёт свойство <see cref="UseTypes"/> и тип значения.</summary>
        ///// <param name="valueType">Тип значения.</param>
        ///// <param name="useBaseTypes">Значение для свойства <see cref="DictionaryTypeConverter.UseBasicTypes"/>.</param>
        //public DictionaryConverterExtension(bool useBaseTypes, Type valueType)
        //    : this(useBaseTypes)
        //{
        //    ValueType = valueType;
        //}

        ///// <summary>Задаёт тип ключа, тип значения и свойство <see cref="UseTypes"/>.</summary>
        ///// <param name="valueType">Тип значения.</param>
        ///// <param name="useBaseTypes">Значение для свойства <see cref="DictionaryTypeConverter.UseBasicTypes"/>.</param>
        //public DictionaryConverterExtension(Type valueType, bool useBaseTypes)
        //    : this(useBaseTypes, valueType)
        //{ }

        ///// <summary>Создаёт экземпляр со свойствами по умолчанию.
        ///// Автоматически начинается инициализация объекта.</summary>
        //public DictionaryConverterExtension() => BeginInit();

        /// <summary>Создаёт экземпляр Расширения Разметки.</summary>
        public DictionaryConverterExtension() { }
    }
}
