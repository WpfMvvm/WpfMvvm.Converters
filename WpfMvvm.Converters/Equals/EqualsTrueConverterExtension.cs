using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр конвертера из свойства <see cref="EqualsTrueConverter.Instance"/> или <see cref="EqualsTrueConverter.NotInstance"/>.</summary>
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class EqualsTrueConverterExtension : MarkupExtension
    {
        /// <summary>Задаёт какой конвертер возвращать.</summary>
        public bool IsTrue { get; set; }

        public EqualsTrueConverterExtension()
            : this(true)
        { }

        public EqualsTrueConverterExtension(bool isTrue)
        {
            IsTrue = isTrue;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => IsTrue 
            ? (IValueConverter) EqualsTrueConverter.Instance
            : EqualsTrueConverter.NotInstance;
    }
}
