using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр <see cref="DebugConverter"/>.<br/>
    /// Если задан <see cref="Converter"/>, то они соединяются в цепочку.</summary>
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class DebugConverterExtension : MarkupExtension
    {

        /// <summary>Конвертер для цепочки.<br/>
        /// Если <see langword="null"/> - возвращается <see cref="DebugConverter.Instance"/>.</summary>
        public IValueConverter Converter { get; set; }

        /// <summary>Последовательность конвертеров в цепочке.</summary>
        public AfterBeforeEnum AfterBefore
        {
            get => afterBefore;
            set
            {
                if (!Enum.IsDefined(typeof(AfterBeforeEnum), value))
                    throw new ArgumentException("Недопустимое значение.", nameof(value));

                afterBefore = value;
            }
        }
        private AfterBeforeEnum afterBefore = AfterBeforeEnum.After;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Converter == null)
                return DebugConverter.Instance;

            var list = new List<IValueConverter>();

            if (AfterBefore.HasFlag(AfterBeforeEnum.Before))
                list.Add(DebugConverter.Instance);

            list.Add(Converter);

            if (AfterBefore.HasFlag(AfterBeforeEnum.After))
                list.Add(DebugConverter.Instance);

            return new ReadOnlyChainOfConverters(list);
        }
    }
}
