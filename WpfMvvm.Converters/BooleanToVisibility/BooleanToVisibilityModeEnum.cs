using System;
using System.Windows;

namespace WpfMvvm.Converters
{
    /// <summary>Перечисление какой из конвертеров надо использовать.</summary>
    [Flags]
    public enum BooleanToVisibilityModeEnum
    {
        /// <summary>С <see cref="Visibility.Collapsed"/> без инверсии.</summary>
        Normal = 0,
        /// <summary>С <see cref="Visibility.Collapsed"/> и инверсией.</summary>
        Not = 1,
        /// <summary>С <see cref="Visibility.Hidden"/> без инверсии.</summary>
        Hidden = 2,
        /// <summary>С <see cref="Visibility.Hidden"/> и инверсией.</summary>
        NotHiden = 3
    }
}
