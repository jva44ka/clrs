using System;
using System.Linq;

namespace CLRS.Core.Structures
{
    public class Entry<TKey, TValue>
    {
        public int _prev;
        public int _next;
        public TKey _key;
        public TValue _value;

        public Entry()
        { }

        public Entry(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }
    }

    public class HashTable<TKey, TValue>
    {
        /// <summary>
        ///     Стартовый размер массива по умолчанию
        /// </summary>
        private const int InitArraysSize = 8;

        // Элементы вместе с ключем и ссылками на предыдущий/следущий элементы в списке (если есть коллизия)
        private Entry<TKey, TValue>[] _entries;

        public int Capacity => _entries.Length;
        public int Count => _entries.Count(el => el != null); 
        public double Fullness => (double)Count / _entries.Length;

        public HashTable()
        {
            InitArray();
        }

        public object this[TKey key]
        {
            get
            {
                var entriesIndex = GetEntriesIndex(key);

                if (_entries[entriesIndex] != null)
                    return _entries[entriesIndex]._value;
                else
                    return null;
            }
        }

        public void Insert(TKey key, TValue value)
        {
            if (IsExistsKey(key))
                throw new ArgumentException("Element with equal key is already exists");

            // TODO: Вынести?
            if (Fullness > 0.7)
                Resize(Capacity * 2);

            var entriesIndex = GetEntriesIndex(key);

            if (_entries[entriesIndex] != null)
            {
                // TODO: Тут коллизия, обработать
            }

            _entries[entriesIndex] = new Entry<TKey, TValue>(key, value);
        }

        public void Remove(TKey key)
        {
            if (!IsExistsKey(key))
                throw new ArgumentException("Element with this key is not exists in hash table");

            // TODO: Вынести?
            if (Fullness < 0.7)
                Resize(Capacity / 2);

            var entriesIndex = GetEntriesIndex(key);

            _entries[entriesIndex] = null;
        }

        bool IsExistsKey(TKey key)
        {
            foreach (var entry in _entries.Where(e => e != null))
                if (key.Equals(entry._key))
                    return true;

            return false;
        }

        /// <summary>
        ///     Получаем индекс для массива Entries для конкретного ключа
        /// </summary>
        int GetEntriesIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode()) % Capacity; 
        }

        #region ArraysHandlers
        void InitArray(int length = InitArraysSize)
        {
            _entries = new Entry<TKey, TValue>[length];
        }

        void Resize(int newLength = InitArraysSize)
        {
            // 1. Поменять алгоритм хеширования
            // 2. Перехешировать существующие значения
        }
        #endregion
    }
}
