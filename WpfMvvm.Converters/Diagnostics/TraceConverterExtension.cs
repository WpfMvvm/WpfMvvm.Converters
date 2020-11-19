using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр <see cref="TraceConverter"/>.<br/>
    /// Если задан <see cref="Converter"/>, то они соединяются в цепочку.</summary>
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class TraceConverterExtension : MarkupExtension
    {

        /// <summary>Заголовок выводимого сообщения.</summary>
        public string Title { get; set; }


        /// <summary>Конвертер для цепочки.<br/>
        /// Если <see langword="null"/> - возвращается <see cref="TraceConverter.Instance"/>.</summary>
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

        /// <summary>Возвращает экземпляр <see cref="TraceConverter"/> или цепочку
        /// <see cref="ReadOnlyChainOfConverters"/> с <see cref="TraceConverter"/>
        /// и <see cref="Converter"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Если <see cref="Converter"/>=<see langword="null"/>, то возвращается <see cref="TraceConverter"/>.<br/>
        /// Иначе создаётся цепочка <see cref="ReadOnlyChainOfConverters"/> с <see cref="TraceConverter"/>
        /// и <see cref="Converter"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var converter = string.IsNullOrWhiteSpace(Title)
                ? TraceConverter.Instance
                : new TraceConverter(Title);


            if (Converter == null)
                return converter;

            var list = new List<IValueConverter>();

            if (AfterBefore.HasFlag(AfterBeforeEnum.Before))
                list.Add(converter);

            list.Add(Converter);

            if (AfterBefore.HasFlag(AfterBeforeEnum.After))
                list.Add(converter);

            return new ReadOnlyChainOfConverters(list);
        }


        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// со значениями свойств по умолчанию.</summary>
        public TraceConverterExtension() { }

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданным значением <see cref="Title"/>.</summary>
        /// <param name="title">Значение для свойства <see cref="Title"/>.</param>
        public TraceConverterExtension(string title)
        {
            Title = title;
        }

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданным значением <see cref="AfterBefore"/>.</summary>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        public TraceConverterExtension(AfterBeforeEnum afterBefore)
            => AfterBefore = afterBefore;

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданным значением <see cref="Title"/> и <see cref="AfterBefore"/>.</summary>
        /// <param name="title">Значение для свойства <see cref="Title"/>.</param>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        public TraceConverterExtension(string title, AfterBeforeEnum afterBefore)
            : this(title)
            => AfterBefore = afterBefore;

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданным значением <see cref="Converter"/>.</summary>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        public TraceConverterExtension(IValueConverter converter)
            => Converter = converter;

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданным значением <see cref="Title"/> и <see cref="Converter"/>.</summary>
        /// <param name="title">Значение для свойства <see cref="Title"/>.</param>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        public TraceConverterExtension(string title, IValueConverter converter)
                   : this(title)
             => Converter = converter;

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданными значениями свойств.</summary>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        public TraceConverterExtension(AfterBeforeEnum afterBefore, IValueConverter converter)
            : this(afterBefore)
            => Converter = converter;

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданными значениями свойств.</summary>
        /// <param name="title">Значение для свойства <see cref="Title"/>.</param>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        public TraceConverterExtension(string title, AfterBeforeEnum afterBefore, IValueConverter converter)
            : this(title, afterBefore)
            => Converter = converter;

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданными значениями свойств.</summary>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        public TraceConverterExtension(IValueConverter converter, AfterBeforeEnum afterBefore)
            : this(afterBefore, converter)
        { }

        /// <summary>Создаёт экземпляр <see cref="TraceConverterExtension"/> 
        /// с заданными значениями свойств.</summary>
        /// <param name="title">Значение для свойства <see cref="Title"/>.</param>
        /// <param name="converter">Значение для свойства <see cref="Converter"/>.</param>
        /// <param name="afterBefore">Значение для свойства <see cref="AfterBefore"/>.</param>
        public TraceConverterExtension(string title, IValueConverter converter, AfterBeforeEnum afterBefore)
            : this(title, afterBefore, converter)
        { }
    }
}
