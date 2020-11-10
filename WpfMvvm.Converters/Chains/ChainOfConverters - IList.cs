using System.Collections;
using System.Collections.Generic;
using System.Windows.Data;

namespace WpfMvvm.Converters
{
    // Реализация IList.
    // Создана Студией из "Быстрых Решений".
    public partial class ChainOfConverters : IList<IValueConverter>
    {
        /// <summary>Индексатор.</summary>
        /// <param name="index">Целый индекс.</param>
        /// <returns>Конвертер по индексу из списка <see cref="Converters"/>.</returns>
        public IValueConverter this[int index]
        {
            get => ((IList<IValueConverter>)Converters)[index];
            set => ((IList<IValueConverter>)Converters)[index] = value;
        }

        /// <summary>Количество элементов в списке <see cref="Converters"/>.</summary>
        public int Count => ((ICollection<IValueConverter>)Converters).Count;

        /// <summary>Всегда <see langword="true"/>.</summary>
        public bool IsReadOnly => ((ICollection<IValueConverter>)Converters).IsReadOnly;

        /// <summary>Добавляет конвертер в список <see cref="Converters"/>.</summary>
        /// <param name="item">Добавляемый конвертер.</param>
        public void Add(IValueConverter item)
        {
            ((ICollection<IValueConverter>)Converters).Add(item);
        }

        /// <summary>Очищает список <see cref="Converters"/>.</summary>
        public void Clear()
        {
            ((ICollection<IValueConverter>)Converters).Clear();
        }

        /// <summary>Определяет, содержит ли список <see cref="Converters"/> <paramref name="item"/>.</summary>
        /// <param name="item">Конвертер для поиска в списке <see cref="Converters"/>.</param>
        /// <returns><see langword="true"/>, если в списке <see cref="Converters"/> есть конвертер <paramref name="item"/>;<br/>
        /// в противном случае — <see langword="false"/>.</returns>
        public bool Contains(IValueConverter item)
        {
            return ((ICollection<IValueConverter>)Converters).Contains(item);
        }

        /// <summary>Копирует элементы списка <see cref="Converters"/>  в массив <paramref name="array"/>.</summary>
        /// <param name="array">Одномерный массив принимающий тип <see cref="IValueConverter"/>.</param>
        /// <param name="arrayIndex">Отсчитываемый от нуля индекс в массиве array, указывающий начало копирования.</param>
        public void CopyTo(IValueConverter[] array, int arrayIndex)
        {
            ((ICollection<IValueConverter>)Converters).CopyTo(array, arrayIndex);
        }

        /// <summary>Перечислитель списка <see cref="Converters"/>.</summary>
        /// <returns>Перечислитель <see cref="Converters"/></returns>
        public IEnumerator<IValueConverter> GetEnumerator()
        {
            return ((IEnumerable<IValueConverter>)Converters).GetEnumerator();
        }

        /// <summary> Определяет индекс <paramref name="item"/> в списке <see cref="Converters"/>.</summary>
        /// <param name="item">Конвертер для поиска в списке <see cref="Converters"/>.</param>
        /// <returns>Индекс <paramref name="item"/>, если он найден в списке;<br/>
        /// в противном случае — значение -1.</returns>
        public int IndexOf(IValueConverter item)
        {
            return ((IList<IValueConverter>)Converters).IndexOf(item);
        }

        /// <summary>Вставляет <paramref name="item"/> в список <see cref="Converters"/> по индексу <paramref name="index"/>.</summary>
        /// <param name="index">Отсчитываемый от нуля индекс, по которому следует вставить <paramref name="item"/>.</param>
        /// <param name="item">Конвертер вставляемый в список <see cref="Converters"/>.</param>
        public void Insert(int index, IValueConverter item)
        {
            ((IList<IValueConverter>)Converters).Insert(index, item);
        }

        /// <summary>Удаляет первое вхождение <paramref name="item"/> из списка <see cref="Converters"/>.</summary>
        /// <param name="item">Конвертер, который необходимо удалить из списка <see cref="Converters"/>.</param>
        /// <returns>Значение <see langword="true"/>, если <paramref name="item"/> успешно удален из списка <see cref="Converters"/>;<br/>
        /// в противном случае — значение false.</returns>
        public bool Remove(IValueConverter item)
        {
            return ((ICollection<IValueConverter>)Converters).Remove(item);
        }

        /// <summary>Удаляет из списка <see cref="Converters"/> конвертер по индексу <paramref name="index"/>.</summary>
        /// <param name="index">Отсчитываемый от нуля индекс удаляемого конвертера.</param>
        public void RemoveAt(int index)
        {
            ((IList<IValueConverter>)Converters).RemoveAt(index);
        }

        /// <summary>Перечислитель списка <see cref="Converters"/>.</summary>
        /// <returns>Перечислитель <see cref="Converters"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Converters).GetEnumerator();
        }
    }

}
