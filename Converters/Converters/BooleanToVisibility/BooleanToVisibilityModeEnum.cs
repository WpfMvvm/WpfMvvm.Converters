using System;
using System.Windows;

namespace WpfMvvm.Converters
{
    /// <summary>Перечисление какой из конвертеров надо использовать.</summary>
    [Flags]
    public enum BooleanToVisibilityModeEnum
    {
        /// <summary>С <see cref="Visibility.Collapsed"/> и без инверсии.</summary>
        Normal = 0,
        /// <summary>С инверсией.</summary>
        Not = 1,
        /// <summary>С <see cref="Visibility.Hidden"/>.</summary>
        Hidden = 2,
        /// <summary>С <see cref="Visibility.Hidden"/> и инверсией.</summary>
        NotHiden = 3
    }
}
