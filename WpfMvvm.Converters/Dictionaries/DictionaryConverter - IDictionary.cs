using System;
using System.Collections;

namespace WpfMvvm.Converters
{
    // Реализация IDictionary. Создана Быстрыми Решениями Visual Studio.
    public partial class DictionaryConverter : IDictionary
    {
        /// <summary>Индексатор. Предоставляет доступ по индексу к словарю <see cref="Dictionary"/>.</summary>
        /// <param name="key">Индекс - ключ.</param>
        /// <returns>Элемент с указанным ключом либо <see langword="null"/>, если такого ключа не существует.</returns>
        public object this[object key] { get => Dictionary[key]; set => Dictionary[key] = value; }

        /// <summary>Коллекция ключей <see cref="Dictionary"/>.</summary>
        public ICollection Keys => Dictionary.Keys;

        /// <summary>Коллекция значений <see cref="Dictionary"/>.</summary>
        public ICollection Values => Dictionary.Values;

        /// <summary>Возвращает <see langword="true"/>, если словарь <see cref="Dictionary"/> неизменяемый.</summary>
        public bool IsReadOnly => Dictionary.IsReadOnly;

        /// <summary>Возвращает <see langword="true"/>, если словарь <see cref="Dictionary"/> фиксированного размера.</summary>
        public bool IsFixedSize => Dictionary.IsFixedSize;

        /// <summary>Возвращает количество элементов в словаре <see cref="Dictionary"/>.</summary>
        public int Count => Dictionary.Count;

        /// <summary>Получает объект, с помощью которого можно синхронизировать доступ
        /// к словарю <see cref="Dictionary"/>.</summary>
        public object SyncRoot => Dictionary.SyncRoot;

        /// <summary>Возвращает значение, показывающее, является ли доступ к словарю <see cref="Dictionary"/> синхронизированным (потокобезопасным).</summary>
        public bool IsSynchronized => Dictionary.IsSynchronized;

        /// <summary>Добавляет элемент с указанными ключом и значением в словарь <see cref="Dictionary"/>.</summary>
        /// <param name="key">Объект используется в качестве ключа добавляемого элемента.</param>
        /// <param name="value">Объект используется в качестве значения добавляемого элемента</param>
        public void Add(object key, object value) => Dictionary.Add(key, value);

        /// <summary>Очищает словарь <see cref="Dictionary"/>.</summary>
        public void Clear() => Dictionary.Clear();

        /// <summary>Определяет, содержится ли элемент с указанным ключом в словаре <see cref="Dictionary"/>.</summary>
        /// <param name="key">Искомый ключ.</param>
        /// <returns>Значение true, если в словаре <see cref="Dictionary"/> содержится элемент с данным 
        /// ключом; в противном случае — значение false.</returns>
        public bool Contains(object key) => key!=null && Dictionary.Contains(key);

        /// <summary>Копирует элементы словаря <see cref="Dictionary"/> в массив <see cref="Array"/>, 
        /// начиная с указанного индекса массива System.Array.</summary>
        /// <param name="array">Одномерный массив <see cref="Array"/>, в который копируются элементы из <see cref="Dictionary"/>.
        /// Массив должен иметь индексацию, начинающуюся с нуля.</param>
        /// <param name="index">Отсчитываемый от нуля индекс в массиве <paramref name="array"/>, указывающий начало копирования.</param>
        public void CopyTo(Array array, int index) => Dictionary.CopyTo(array, index);

        /// <summary>Возвращает перечислитель</summary>
        /// <returns>Перечислитель словаря <see cref="Dictionary"/>.</returns>
        public IDictionaryEnumerator GetEnumerator() => Dictionary.GetEnumerator();

        /// <summary>Удаляет элемент с указанным ключом из словаря <see cref="Dictionary"/>.</summary>
        /// <param name="key">Ключ элемента, который требуется удалить.</param>
        public void Remove(object key) => Dictionary.Remove(key);

        /// <summary>Возвращает перечислитель</summary>
        /// <returns>Перечислитель словаря <see cref="Dictionary"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();
    }

}
