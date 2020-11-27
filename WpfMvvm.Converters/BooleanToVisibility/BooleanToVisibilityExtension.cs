using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Предоставляет один из экземпляров <see cref="BooleanNotConverter"/>:
    /// <see cref="BooleanToVisibilityConverter.Instance"/>, <see cref="BooleanToVisibilityHiddenConverter.Instance"/>,
    /// <see cref="BooleanToVisibilityConverter.NotInstance"/>, <see cref="BooleanToVisibilityHiddenConverter.NotInstance"/>.</summary>
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class BooleanToVisibilityExtension : MarkupExtension
    {
        /// <summary>Какой из конвертеров будет использован.</summary>
        public BooleanToVisibilityModeEnum Mode { get; set; }

        /// <summary>Создаёт экземпляр расширения разметки с <see cref="Mode"/>=<see cref="BooleanToVisibilityModeEnum.Normal"/>.</summary>
        public BooleanToVisibilityExtension()
            => Mode = BooleanToVisibilityModeEnum.Normal;

        /// <summary>Создаёт экземпляр расширения разметки с заданным значением <see cref="Mode"/>.</summary>
        /// <param name="mode">Значение для <see cref="Mode"/>.</param>
        /// <exception cref="ArgumentException">Если <paramref name="mode"/> не член <see cref="BooleanToVisibilityModeEnum"/>.</exception>
        public BooleanToVisibilityExtension(BooleanToVisibilityModeEnum mode)
        {
            if (!Enum.IsDefined(typeof(BooleanToVisibilityModeEnum), mode))
                throw SetModeException;
            Mode = mode;
        }

        /// <summary>Возвращает конвертер соответствующий заданному значению <see cref="Mode"/>.</summary>
        /// <param name="serviceProvider">Вспомогательный объект поставщика служб,
        /// способный предоставлять службы для расширения разметки.<para/>
        /// Не используется.</param>
        /// <returns>Возвращает для значений <see cref="Mode"/>:<br/>
        /// <see cref="BooleanToVisibilityModeEnum.Normal"/> - <see cref="BooleanToVisibilityConverter.Instance"/>,<br/>
        /// <see cref="BooleanToVisibilityModeEnum.Not"/> - <see cref="BooleanToVisibilityConverter.NotInstance"/>,<br/>
        /// <see cref="BooleanToVisibilityModeEnum.Hidden"/> - <see cref="BooleanToVisibilityHiddenConverter.Instance"/>,<br/>
        /// <see cref="BooleanToVisibilityModeEnum.NotHiden"/> - <see cref="BooleanToVisibilityHiddenConverter.NotInstance"/>.
        /// </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            switch (Mode)
            {
                case BooleanToVisibilityModeEnum.Normal:
                    return BooleanToVisibilityConverter.Instance;
                case BooleanToVisibilityModeEnum.Not:
                    return BooleanToVisibilityConverter.NotInstance;
                case BooleanToVisibilityModeEnum.Hidden:
                    return BooleanToVisibilityHiddenConverter.Instance;
                case BooleanToVisibilityModeEnum.NotHiden:
                    return BooleanToVisibilityHiddenConverter.NotInstance;
                default:
                    throw SetModeException;
            }

        }

        /// <summary>Ошибка при присвоении свойству <see cref="Mode"/> недопустимого значения.</summary>
        public static ArgumentException SetModeException { get; } = new ArgumentException("Недопустимое значение", nameof(Mode));
    }
}
