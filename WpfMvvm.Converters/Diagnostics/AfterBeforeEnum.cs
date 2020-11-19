using System;

namespace WpfMvvm.Converters
{
    /// <summary>Перечисление расположения <see cref="TraceConverter"/> по отношению к другому конвертеру.</summary>
    [Flags]
    public enum AfterBeforeEnum
    {
        /// <summary>После.</summary>
        After = 1, 
        /// <summary>Спереди.</summary>
        Before = 2, 
        /// <summary>И спереди, и после.</summary>
        AfterAndBefore = 3
    }
}
