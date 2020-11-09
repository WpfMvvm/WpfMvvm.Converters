using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Предоставляет экземпляр <see cref="BooleanNotConverter"/> из <see cref="BooleanNotConverter.Instance"/>.</summary>
    [MarkupExtensionReturnType(typeof(BooleanNotConverter))]
    public class BooleanNotConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => BooleanNotConverter.Instance;
    }
}
