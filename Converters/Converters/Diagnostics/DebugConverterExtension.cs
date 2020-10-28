using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр <see cref="DebugConverter"/>.<br/>
    /// Если задан <see cref="Converter"/>, то они соединяются в цепочку.</summary>
    public class DebugConverterExtension : MarkupExtension
    {
        /// <summary>Конвертер для цепочки.<br/>
        /// Если <see langword="null"/> - возвращается <see cref="DebugConverter.Instance"/>.</summary>
        public IValueConverter Converter { get; set; }

        /// <summary>Последовательность конвертеров в цепочке:<br/>
        /// <see langword="true"/> - <see cref="DebugConverter"/> в начале;<br/>
        /// <see langword="false"/> - <see cref="DebugConverter"/> в конце;<br/>
        /// <see langword="null"/> - <see cref="DebugConverter"/> и перед, и после <see cref="Converter"/>.</summary>
        public bool? IsAfterOrBefore { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Converter == null)
                return DebugConverter.Instance;

            var list = new List<IValueConverter>();

            if (IsAfterOrBefore != false)
                list.Add(DebugConverter.Instance);

            list.Add(Converter);
 
            if (IsAfterOrBefore != true)
                list.Add(DebugConverter.Instance);

            return new ReadOnlyChainOfConverters(list);
       }
    }
}
