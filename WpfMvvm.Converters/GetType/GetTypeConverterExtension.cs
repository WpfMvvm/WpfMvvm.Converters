using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает <see cref="GetTypeConverter.Instance"/>.</summary>
    [MarkupExtensionReturnType(typeof(GetTypeConverter))]
    public class GetTypeConverterExtension : MarkupExtension
    {
        /// <summary>Возвращает <see cref="GetTypeConverter.Instance"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns><see cref="GetTypeConverter.Instance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => GetTypeConverter.Instance;

        /// <summary>Создаёт экземпляр <see cref="GetTypeConverterExtension"/>.</summary>
        public GetTypeConverterExtension() { }
    }
}
