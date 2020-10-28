using System;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает один из экземпляров <see cref="DictionaryTypeConverter.InstanceUseBaseTypes"/> или <see cref="DictionaryTypeConverter.InstanceNotUseBaseTypes"/>.</summary>
    public class DictionaryTypeConverterExtension : MarkupExtension
    {
        /// <summary>Указывает какой экземпляр будет использован для возвращения.</summary>
        /// <value>Если <see langword="true"/>, то используется <see cref="DictionaryTypeConverter.InstanceUseBaseTypes"/>.<br/>
        /// Если <see langword="false"/> - <see cref="DictionaryTypeConverter.InstanceNotUseBaseTypes"/>.</value>
        public bool UseBasicTypes { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
            => UseBasicTypes 
            ? DictionaryTypeConverter.InstanceUseBaseTypes
            : DictionaryTypeConverter.InstanceNotUseBaseTypes;

        public DictionaryTypeConverterExtension()
        { }

        public DictionaryTypeConverterExtension(bool useBasicTypes)
            => UseBasicTypes = useBasicTypes;
    }

}
