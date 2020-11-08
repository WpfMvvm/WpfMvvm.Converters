using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    // Реализация IReadOnlyList.
    // Создана Студией из "Быстрых Решений".
    public partial class ReadOnlyChainOfConverters : IReadOnlyList<IValueConverter>
    {
        public IValueConverter this[int index] => Converters[index];

        public int Count => Converters.Count;

        public IEnumerator<IValueConverter> GetEnumerator()
        {
            return Converters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Converters).GetEnumerator();
        }
    }

}
