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

        /// <summary>Создаёт экземпляр расширения разметки с <see cref="IsTrue"/>=<see langword="true"/>.</summary>
        public EqualsTrueConverterExtension()
            : this(true)
        { }

        /// <summary>Создаёт экземпляр расширения разметки с <see cref="IsTrue"/>=<paramref name="isTrue"/>.</summary>
        /// <param name="isTrue">Значение для <see cref="IsTrue"/>.</param>
        public EqualsTrueConverterExtension(bool isTrue)
        {
            IsTrue = isTrue;
        }

        /// <summary>Возвращает экземпляр конвертера.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Возвращает экземпляр конвертера для значений <see cref="IsTrue"/>: <br/>
        /// <see langword="true"/> - <see cref="EqualsTrueConverter.Instance"/>;<br/>
        /// <see langword="false"/> - <see cref="EqualsTrueConverter.NotInstance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => IsTrue 
            ? EqualsTrueConverter.Instance
            : EqualsTrueConverter.NotInstance;
    }
}
