using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает <see cref="EnumValuesConverter.Instance"/>.</summary>
    public class EnumValuesConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
            => EnumValuesConverter.Instance;
    }
}
