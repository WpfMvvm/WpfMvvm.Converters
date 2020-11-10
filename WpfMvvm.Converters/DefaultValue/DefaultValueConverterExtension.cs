using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр <see cref="DefaultValueConverter"/> с заданными типами.<br/>
    /// Для получения экземпляра используется метод <see cref="StaticMethodsOfConverters.GetValueTypeConverter(Type, Type)"/>.</summary>
    [MarkupExtensionReturnType(typeof(DefaultValueConverter))]
    public class DefaultValueConverterExtension : MarkupExtension
    {
        /// <inheritdoc cref="DefaultValueConverter.SourceType"/>
        public Type SourceType { get; set; }

        /// <inheritdoc cref="DefaultValueConverter.TargetType"/>
        public Type TargetType { get; set; }

        /// <summary>Задаёт будут ли исключения при невозможности преобразования значения к указанному типу.</summary>
        /// <remarks><see langword="true"/> - создаётся и возвращается экземпляр <see cref="DefaultValueConverter"/>,<br/>
        /// <see langword="false"/> - </remarks>
        public bool IsWithNoExceptions { get; set; }

        /// <summary>Конструктор по умолчанию.</summary>
        public DefaultValueConverterExtension() { }

        /// <summary>Конструктор с заданием значений свойствам.</summary>
        /// <param name="targetType">Значение для свойства <see cref="TargetType"/>.</param>
        /// <param name="sourceType">Значение для свойства <see cref="SourceType"/>.</param>
        public DefaultValueConverterExtension(Type targetType, Type sourceType) : this(targetType)
        {
            SourceType = sourceType;
        }

        /// <summary>Конструктор с заданием типа целевого свойства.</summary>
        /// <param name="targetType">Значение для свойства <see cref="TargetType"/>.</param>
        public DefaultValueConverterExtension(Type targetType) : this()
        {
            TargetType = targetType;
        }

        /// <summary>Возвращает экземпляр конвертера для типов <see cref="SourceType"/> и <see cref="TargetType"/>
        /// из метода <see cref="StaticMethodsOfConverters.GetValueTypeConverter(Type, Type)"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Экземпляр из метода <see cref="StaticMethodsOfConverters.GetValueTypeConverter(Type, Type)"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return StaticMethodsOfConverters.GetValueTypeConverter(SourceType, TargetType);
        }
    }
}
