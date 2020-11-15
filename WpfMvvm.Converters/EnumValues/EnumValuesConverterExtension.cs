using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает <see cref="EnumValuesConverter.Instance"/>.</summary>
    [MarkupExtensionReturnType(typeof(EnumValuesConverter))]
    public class EnumValuesConverterExtension : MarkupExtension
    {
        /// <summary>Возвращает конвертер из свойства <see cref="EnumValuesConverter.Instance"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns><see cref="EnumValuesConverter.Instance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => EnumValuesConverter.Instance;

        /// <summary>Создаёт экземпляр <see cref="EnumValuesConverterExtension"/>.</summary>
        public EnumValuesConverterExtension() { }
    }
}
