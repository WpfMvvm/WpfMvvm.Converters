using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    // Свойства
    [DefaultBindingProperty(nameof(Binding))]
    public partial class DictionaryConverterExtension : MarkupExtension
    {
        /// <summary>Привязка для словаря.</summary>
        public Binding Binding { get; set; }


        /// <summary>Определяет какой конвертер бyдет возвращён.</summary>
        [DefaultValue(UseTypesEnum.NotType)]
        public UseTypesEnum UseTypes
        {
            get => _useTypes;
            set
            {
                if (!Enum.IsDefined(typeof(UseTypesEnum), value))
                    throw new ArgumentException("Недопустимое значение", nameof(value));
                _useTypes = value;
            }
        }
        private UseTypesEnum _useTypes;
    }
}
