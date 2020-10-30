using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfMvvm.Converters
{
    /// <summary>Предоставляет один из экземпляров <see cref="BooleanNotConverter"/>: <see cref="BooleanToVisibilityConverter.Instance"/>, <see cref="BooleanToVisibilityHiddenConverter.Instance"/>, <see cref="BooleanToVisibilityConverter.NotInstance"/>, <see cref="BooleanToVisibilityHiddenConverter.NotInstance"/>.</summary>
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class BooleanToVisibilityExtension : MarkupExtension
    {
        /// <summary>Какой из конвертеров будет использован.</summary>
        public BooleanToVisibilityModeEnum Mode { get; set; }

        public BooleanToVisibilityExtension() { }

        public BooleanToVisibilityExtension(BooleanToVisibilityModeEnum mode)
        {
            if (!Enum.IsDefined(typeof(BooleanToVisibilityModeEnum), mode))
                throw new ArgumentException(nameof(Mode));
            Mode = mode;
        }

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
                    throw new ArgumentException(nameof(Mode));
            }

        }
    }
}
