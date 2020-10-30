using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр <see cref="ValueTypeConverter"/> с заданными типами.<br/>
    /// Для получения экземпляра используется метод <see cref="StaticMethodsOfConverters.GetValueTypeConverter(Type, Type)"/>.</summary>
    [MarkupExtensionReturnType(typeof(ValueTypeConverter))]
    public class ValueTypeConverterExtension : MarkupExtension
    {
        /// <inheritdoc cref="ValueTypeConverter.SourceType"/>
        public Type SourceType { get; set; }

        /// <inheritdoc cref="ValueTypeConverter.TargetType"/>
        public Type TargetType { get; set; }

        /// <summary>Конструктор по умолчанию.</summary>
        public ValueTypeConverterExtension(){}

        /// <summary>Конструктор с заданием значений свойствам.</summary>
        /// <param name="sourceType">Значение для свойства <see cref="SourceType"/>.</param>
        /// <param name="targetType">Значение для свойства <see cref="TargetType"/>.</param>
        public ValueTypeConverterExtension(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
           return StaticMethodsOfConverters.GetValueTypeConverter(SourceType, TargetType);
        }
    }
}
