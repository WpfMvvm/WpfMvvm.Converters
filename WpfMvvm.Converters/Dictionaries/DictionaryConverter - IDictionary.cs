﻿using System;
using System.Collections;

namespace WpfMvvm.Converters
{
    // Реализация IDictionary. Создана Быстрыми Решениями Visual Studio.
    public partial class DictionaryConverter : IDictionary
    {
        public object this[object key] { get => Dictionary[key]; set => Dictionary[key] = value; }

        public ICollection Keys => Dictionary.Keys;

        public ICollection Values => Dictionary.Values;

        public bool IsReadOnly => Dictionary.IsReadOnly;

        public bool IsFixedSize => Dictionary.IsFixedSize;

        public int Count => Dictionary.Count;

        public object SyncRoot => Dictionary.SyncRoot;

        public bool IsSynchronized => Dictionary.IsSynchronized;

        public void Add(object key, object value) => Dictionary.Add(key, value);

        public void Clear() => Dictionary.Clear();

        public bool Contains(object key) => Dictionary.Contains(key);

        public void CopyTo(Array array, int index) => Dictionary.CopyTo(array, index);

        public IDictionaryEnumerator GetEnumerator() => Dictionary.GetEnumerator();

        public void Remove(object key) => Dictionary.Remove(key);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();
    }

}
