using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    // Реализация IList.
    // Создана Студией из "Быстрых Решений".
    public partial class ChainOfConverters : IList<IValueConverter>
    {
        public IValueConverter this[int index] { get => ((IList<IValueConverter>)Converters)[index]; set => ((IList<IValueConverter>)Converters)[index] = value; }

        public int Count => ((ICollection<IValueConverter>)Converters).Count;

        public bool IsReadOnly => ((ICollection<IValueConverter>)Converters).IsReadOnly;

        public void Add(IValueConverter item)
        {
            ((ICollection<IValueConverter>)Converters).Add(item);
        }

        public void Clear()
        {
            ((ICollection<IValueConverter>)Converters).Clear();
        }

        public bool Contains(IValueConverter item)
        {
            return ((ICollection<IValueConverter>)Converters).Contains(item);
        }

        public void CopyTo(IValueConverter[] array, int arrayIndex)
        {
            ((ICollection<IValueConverter>)Converters).CopyTo(array, arrayIndex);
        }

        public IEnumerator<IValueConverter> GetEnumerator()
        {
            return ((IEnumerable<IValueConverter>)Converters).GetEnumerator();
        }

        public int IndexOf(IValueConverter item)
        {
            return ((IList<IValueConverter>)Converters).IndexOf(item);
        }

        public void Insert(int index, IValueConverter item)
        {
            ((IList<IValueConverter>)Converters).Insert(index, item);
        }

        public bool Remove(IValueConverter item)
        {
            return ((ICollection<IValueConverter>)Converters).Remove(item);
        }

        public void RemoveAt(int index)
        {
            ((IList<IValueConverter>)Converters).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Converters).GetEnumerator();
        }
    }

}
