using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Возвращает экземпляр конвертера из свойства <see cref="EqualsConverter.Instance"/> или <see cref="EqualsConverter.NotInstance"/>.</summary>
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class EqualsConverterExtension : MarkupExtension
    {
        /// <summary>Задаёт какой конвертер возвращать.</summary>
        public bool IsTrue { get; set; }

        /// <summary>Создаёт экземпляр расширения разметки с <see cref="IsTrue"/>=<see langword="true"/>.</summary>
        public EqualsConverterExtension()
            : this(true)
        { }

        /// <summary>Создаёт экземпляр расширения разметки с <see cref="IsTrue"/>=<paramref name="isTrue"/>.</summary>
        /// <param name="isTrue">Значение для <see cref="IsTrue"/>.</param>
        public EqualsConverterExtension(bool isTrue)
        {
            IsTrue = isTrue;
        }

        /// <summary>Возвращает экземпляр конвертера.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Возвращает экземпляр конвертера для значений <see cref="IsTrue"/>: <br/>
        /// <see langword="true"/> - <see cref="EqualsConverter.Instance"/>;<br/>
        /// <see langword="false"/> - <see cref="EqualsConverter.NotInstance"/>.</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
            => IsTrue 
            ? (IValueConverter) EqualsConverter.Instance
            : EqualsConverter.NotInstance;
    }
}
