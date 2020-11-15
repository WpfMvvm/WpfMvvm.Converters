using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DefaultValue(null)]
        public IValueConverter Converter { get; set; }

        /// <summary>Последовательность конвертеров в цепочке, если задан конвертер в свойстве <see cref="Converter"/>.</summary>
        [DefaultValue(AfterBeforeEnum.After)]
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

        /// <summary>Возвращает экземпляр <see cref="DebugConverter"/> или цепочку
        /// <see cref="ReadOnlyChainOfConverters"/> с <see cref="DebugConverter"/>
        /// и <see cref="Converter"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Если <see cref="Converter"/>=<see langword="null"/>, то возвращается <see cref="DebugConverter"/>.<br/>
        /// Иначе создаётся цепочка <see cref="ReadOnlyChainOfConverters"/> с <see cref="DebugConverter"/>
        /// и <see cref="Converter"/>.</returns>
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


        /// <summary>Создаёт экземпляр <see cref="DebugConverterExtension"/> 
        /// со значениями свойств по умолчанию.</summary>
        public DebugConverterExtension() { }

        /// <summary>Создаёт экземпляр <see cref="DebugConverterExtension"/> 
        /// с заданным значением <see cref="AfterBefore"/>.</summary>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        public DebugConverterExtension(AfterBeforeEnum afterBefore)
            => AfterBefore = afterBefore;

        /// <summary>Создаёт экземпляр <see cref="DebugConverterExtension"/> 
        /// с заданным значением <see cref="Converter"/>.</summary>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        public DebugConverterExtension(IValueConverter converter)
            => Converter = converter;

        /// <summary>Создаёт экземпляр <see cref="DebugConverterExtension"/> 
        /// с заданными значениями свойств.</summary>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        public DebugConverterExtension(AfterBeforeEnum afterBefore, IValueConverter converter)
            : this(afterBefore)
            => Converter = converter;

        /// <summary>Создаёт экземпляр <see cref="DebugConverterExtension"/> 
        /// с заданными значениями свойств.</summary>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        public DebugConverterExtension(IValueConverter converter, AfterBeforeEnum afterBefore)
            : this(afterBefore, converter)
        { }
    }
}
