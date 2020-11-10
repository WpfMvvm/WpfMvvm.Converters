using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    // Реализация IReadOnlyList.
    // Создана Студией из "Быстрых Решений".
    public partial class ReadOnlyChainOfConverters : IReadOnlyList<IValueConverter>
    {
        /// <summary>Индексатор. Только для чтения.</summary>
        /// <param name="index">Целый индекс.</param>
        /// <returns>Конвертер по индексу из списка <see cref="Converters"/>.</returns>
        public IValueConverter this[int index] => Converters[index];

        /// <summary>Количество элементов в списке <see cref="Converters"/>.</summary>
        public int Count => Converters.Count;

        /// <summary>Перечислитель списка <see cref="Converters"/>.</summary>
        /// <returns>Перечислитель <see cref="Converters"/></returns>
        public IEnumerator<IValueConverter> GetEnumerator()
        {
            return Converters.GetEnumerator();
        }

        /// <summary>Перечислитель списка <see cref="Converters"/>.</summary>
        /// <returns>Перечислитель <see cref="Converters"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Converters).GetEnumerator();
        }
    }

}
