using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр конвертера <see cref="EqualsTrueConverter"/> из свойства <see cref="EqualsTrueConverter.Instance"/>.</summary>
    public class EqualsTrueConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => EqualsTrueConverter.Instance;
    }
}
