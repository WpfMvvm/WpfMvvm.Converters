using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает один из экземпляров <see cref="SelectorTypeConverter{TValue}.Instance"/> или <see cref="SelectorTypeConverter{TValue}.InstanceNotUseBaseTypes"/>.</summary>
    public class SelectorTypeConverterExtension : MarkupExtension
    {
        private Type _dataType = typeof(object);

        /// <summary>Тип данных конвертера-селектора.<br/>
        /// По умолчанию - <see cref="object"/>.</summary>
        /// <exception cref="ArgumentNullException">При присвоении <see langword="null"/>.</exception>
        public Type DataType { get => _dataType; set => _dataType = value ?? throw new ArgumentNullException(nameof(value)); }

        /// <summary>Указывает какой экземпляр будет использован для возвращения.</summary>
        /// <value>Если <see langword="true"/>, то используется <see cref="SelectorTypeConverter{TValue}.Instance"/>.<br/>
        /// Если <see langword="false"/> - <see cref="SelectorTypeConverter{TValue}.InstanceNotUseBaseTypes"/>.</value>
        public bool UseBasicTypes { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DataType == null)
                throw new Exception($"{nameof(DataType)}=null: Тип данных должен быть обязательно задан!");

            return UseBasicTypes
                ? GetSelectorTypeConverters(DataType).instance
                : GetSelectorTypeConverters(DataType).instanceNotUseBaseTypes;
        }

        private static readonly Dictionary<Type, (BaseSelectorTypeConverter instance, BaseSelectorTypeConverter instanceNotUseBaseTypes)> selectors
            = new Dictionary<Type, (BaseSelectorTypeConverter instance, BaseSelectorTypeConverter instanceNotUseBaseTypes)>();

        public static (BaseSelectorTypeConverter instance, BaseSelectorTypeConverter instanceNotUseBaseTypes) GetSelectorTypeConverters(Type dataType)
        {
            if (dataType == null)
                throw new ArgumentNullException(nameof(dataType));

            if (!selectors.TryGetValue(dataType, out var converters))
            {
                var selectorType = typeof(SelectorTypeConverter<>);
                var selectorClosedType = selectorType.MakeGenericType(dataType);
                var propertyInstance = selectorClosedType.GetProperty(nameof(SelectorTypeConverter<object>.Instance), BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                var instance = (BaseSelectorTypeConverter)propertyInstance.GetValue(null);
                var propertyInstanceNotUseBaseTypes = selectorClosedType.GetProperty(nameof(SelectorTypeConverter<object>.InstanceNotUseBaseTypes), BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                var instanceNotUseBaseTypes = (BaseSelectorTypeConverter)propertyInstanceNotUseBaseTypes.GetValue(null);
                converters = (instance, instanceNotUseBaseTypes);
                selectors.Add(dataType, converters);
            }

            return converters;
        }
    }


}
