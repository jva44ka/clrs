using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cormen.Core.Structures
{
    public class HashTable<TKey, TValue>
    {
        private struct Entry
        {
            public int hashCode;
            public int prev;
            public int next;
            public TKey key;
            public TValue value;
        }

        // Индексы элементов в entries
        private int[] _buckets;
        // Элементы вместе с ключем и ссылками на предыдущий/следущий элементы в списке (если есть коллизия)
        private Entry[] _entries;
        private int _count;

        public int Capacity => _entries.Length;
        public double Fullness => (double)Count / _entries.Length;
        public int Count => _entries.Count(el => !el.Equals(default(Entry))); 

        public HashTable()
        {
            InitArrays();
        }

        public object this[TKey key]
        {
            get
            {
                var targetBucket = GetBucketNum(key);

                if (!_entries[_buckets[targetBucket]].Equals(default(Entry)))
                    return _entries[_buckets[targetBucket]].value;
                else
                    return null;
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (IsExistsKey(key))
                throw new ArgumentException("Element with equal key is already exists");

            var hashCode = GetHashCode(key);
            var targetBucket = GetBucketNum(key);

            int index;
            if (Fullness > 0.7)
            {
                index = _count;
            }
            else
            {
                Resize(Count * 2);
                index = _count;
            }
            _count++;
            _entries[index].hashCode = hashCode;
            _entries[index].key = key;
            _entries[index].value = value;
            _buckets[targetBucket] = index;
        }

        public void Remove(TKey key)
        {
            if (!IsExistsKey(key))
                throw new ArgumentException("Element with this key is not exists in hash table");

            var hashCode = GetHashCode(key);
            var targetBucket = GetBucketNum(key);
            var targetIndex = _buckets[targetBucket];

            _entries[targetIndex].hashCode = 0;
            _entries[targetIndex].key = default(TKey);
            _entries[targetIndex].value = default(TValue);
            _count--;

            if (Fullness < 0.7)
                Resize(Count / 2);
        }

        bool IsExistsKey(TKey key)
        {
            foreach (var entry in _entries)
                if (key.Equals(entry.key))
                    return true;

            return false;
        }

        // Получаем хеш-результат ключа (хеширование)
        int GetHashCode(TKey key)
        {
            return key.GetHashCode() & 0x7fffffff;
        }

        // Получаем хеш-результат ключа (хеширование)
        int GetBucketNum(TKey key)
        {
            return GetHashCode(key) % 10;

            // Применять эту формулу только с введением перехеширования иначе старые ключи будут ехать после ресайза
            //return (key.GetHashCode() & 0x7fffffff) % Capacity;
        }

        #region ArraysHandlers
        void InitArrays(int length = 10)
        {
            _buckets = new int[length];
            _entries = new Entry[length];
            _count = 0;
        }

        // Вызывать при copacity ~0.7
        void Resize(int length = 10)
        {

        }
        #endregion
    }
}
