using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Предоставляет экземпляр <see cref="ExpressionConverter"/> из <see cref="ExpressionConverter.Instance"/>.</summary>
    [MarkupExtensionReturnType(typeof(object))]
    public class ExpressionConverterExtension : MarkupExtension
    {
        /// <summary>Возвращает <see cref="ExpressionConverter.Instance"/>.</summary>
        /// <param name="serviceProvider">Провайдер данных. Неиспользуется.</param>
        /// <returns><see cref="ExpressionConverter.Instance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => ExpressionConverter.Instance;
    }
}
