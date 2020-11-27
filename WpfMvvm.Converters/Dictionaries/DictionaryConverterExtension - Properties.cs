using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    // Свойства
    //[ContentProperty(nameof(Dictionary))]
    [DefaultBindingProperty(nameof(Binding))]
    public partial class DictionaryConverterExtension : MarkupExtension
    {
        //private Type _valueType = typeof(object);
        //private Type _keyType = typeof(object);

        /// <summary>Привязка для словаря.</summary>
        public Binding Binding { get; set; }

        ///// <summary>Словарь для заполнения в XAML.</summary>
        //public IDictionary Dictionary { get; set; } = new Dictionary<object, object>();

        ///// <summary>Тип ключа словаря<br/>
        ///// Если <see langword="null"/> - используется значение по умолчанию.<br/>
        ///// По умолчанию, - <see cref="object"/>.</summary>
        ///// <remarks>Если заданно значение, то не должен быть или <see langword="null"/>, или <see cref="Type"/>.</remarks>
        //[DefaultValue(typeof(object))]
        //public Type KeyType
        //{
        //    get => _keyType;
        //    set
        //    {
        //        if (value == null)
        //            value = typeof(object);

        //        if (_keyType == value)
        //            return;

        //        _keyType = value;
        //        ChangeTypeDictionary();
        //    }
        //}

        ///// <summary>Тип значения словаря.<br/>
        ///// Если <see langword="null"/> - используется значение по умолчанию.<br/>
        ///// По умолчанию - <see cref="object"/>.</summary>
        //[DefaultValue(typeof(object))]
        //public Type ValueType
        //{
        //    get => _valueType;
        //    set
        //    {
        //        if (value == null)
        //            value = typeof(object);

        //        if (_valueType == value)
        //            return;

        //        _valueType = value;
        //        ChangeTypeDictionary();
        //    }
        //}


        /// <summary>Определяет какой конвертер бyдет возвращён.</summary>
        ///// <value><see langword="null"/> - <see cref="DictionaryConverter"/>,<br/>
        ///// <see langword="true"/> - <see cref="DictionaryTypeConverter"/> с <see cref="DictionaryTypeConverter.UseBasicTypes"/>=<see langword="true"/>,<br/>
        ///// <see langword="false"/> - <see cref="DictionaryTypeConverter"/> с <see cref="DictionaryTypeConverter.UseBasicTypes"/>=<see langword="false"/>.</value>
        ///// <remarks>Если заданно значение, то не должно задаваться значение для <see cref="KeyType"/>.</remarks>
        [DefaultValue(UseTypesEnum.NotType)]
        public UseTypesEnum UseTypes
        {
            get => _useTypes;
            set
            {
                if (!Enum.IsDefined(typeof(UseTypesEnum), value))
                    throw new ArgumentException("Недопустимое значение", nameof(value));
                _useTypes = value;
            }
        }
        private UseTypesEnum _useTypes;


        ///// <summary>Изменение типа словаря <see cref="Dictionary"/>.</summary>
        //private void ChangeTypeDictionary()
        //{
        //    if (UseTypes != null && KeyType != null && KeyType != typeof(Type))
        //        throw new Exception($"Если задано значение {nameof(UseTypes)}, то {nameof(KeyType)} должен быть null или Type.");

        //    var keyType = KeyType;
        //    if (keyType == null)
        //        keyType = UseTypes == null
        //            ? typeof(object)
        //            : typeof(Type);

        //    var dictionaryType = typeof(Dictionary<,>);
        //    var dictionaryClosedType = dictionaryType.MakeGenericType(keyType, ValueType ?? typeof(object));

        //    Dictionary = (IDictionary)Activator.CreateInstance(dictionaryClosedType);
        //}
    }


}
