using System;
using System.Collections.Generic;
using System.Linq;

namespace CLRS.Core.Structures
{
    public class Entry<TKey, TValue>
    {
        internal Entry<TKey, TValue> _prev;
        internal Entry<TKey, TValue> _next;
        internal TKey _key;
        internal TValue _value;

        /// <summary>
        ///     Поиск последнего значения в связанном списке (коллизия)
        /// </summary>
        public Entry<TKey, TValue> Last
        {
            get
            {
                if (_next == null)
                    return this;
                else
                    return _next.Last;
            }
        }

        public Entry(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        /// <summary>
        ///     Поиск значения с заданным ключем в связанном списке (коллизия)
        /// </summary>
        public Entry<TKey, TValue> Find(TKey key)
        {
            if (_key.Equals(key))
                return this;

            if (_next != null)
                return _next.Find(key);
            else
                return null;
        }

        /// <summary>
        ///     Добавление ноды в конец связанного списка (коллизия)
        /// </summary>
        public void AddToList(Entry<TKey, TValue> newNode)
        {
            var lastNode = Last;
            lastNode._next = newNode;
            newNode._prev = lastNode;
        }

        /// <summary>
        ///     Удаление ноды в связанном списке (коллизии)
        /// </summary>
        /// <returns>Головная нода связанного списка</returns>
        public Entry<TKey, TValue> RemoveFromList(TKey key)
        {
            var entry = Find(key);

            //  Удаляемая нода в середине списка
            if (entry._prev != null && entry._next != null)
            {
                entry._prev._next = entry._next;
                entry._next._prev = entry._prev;
                return this;
            }
            // Удаляемая нода в конце списка
            else if (entry._next == null && entry._prev != null)
            {
                entry._prev._next = null;
                return this;
            }
            // Удаляемая нода в начале списка
            else if (entry._next != null && entry._prev == null)
            {
                entry._next._prev = null;
                return entry._next;
            }
            // Удаляемая нода единственная в списке
            else
                return null;
        }

        /// <summary>
        ///     Возвращает все связанные ноды в формате IEnumerable
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Entry<TKey, TValue>> ToEnumerable()
        {
            var count = RecurciveCount(0);
            var array = new Entry<TKey, TValue>[count];
            var currentNode = this;
            for (int i = 0; i < count; i++)
            {
                array[i] = currentNode;
                currentNode = currentNode._next;
            }

            return array;
        }

        int RecurciveCount(int counter)
        {
            counter++;
            if (_next != null) 
                return _next.RecurciveCount(counter);
            else
                return counter;
        }
    }

    public class HashTable<TKey, TValue>
    {
        /// <summary>
        ///     Стартовый размер массива entries по умолчанию
        /// </summary>
        private const int EntriesMinSize = 8;

        /// <summary>
        ///     Нижняя граница коэффициента заполненности, при пересечении которого происходит уменьшение вместимости хеш-таблицы
        /// </summary>
        private const double DownFullnessBorder = 0.4;

        /// <summary>
        ///     Верхняя граница коэффициента заполненности, при пересечении которого происходит увеличение вместимости хеш-таблицы
        /// </summary>
        private const double UpFullnessBorder = 0.8;

        /// <summary>
        ///     Элементы вместе с ключем и ссылками на предыдущий/следущий элементы в списке (если есть коллизия)
        /// </summary>
        private Entry<TKey, TValue>[] _entries = new Entry<TKey, TValue>[EntriesMinSize];

        public int Capacity => _entries.Length;
        public int Count => _entries.Count(el => el != null); 
        public double Fullness => (double)Count / _entries.Length;

        public object this[TKey key]
        {
            get
            {
                var entriesIndex = GetEntriesIndex(key);
                var headEntry = _entries[entriesIndex];
                var entry = headEntry?.Find(key);

                if (entry != null)
                    return entry._value;
                else
                    return null;
            }
        }

        /// <summary>
        ///     Вставка нового значения
        /// </summary>
        public void Insert(TKey key, TValue value)
        {
            if (IsExistsKey(key))
                throw new ArgumentException("Element with equal key is already exists");
            
            if (Fullness > UpFullnessBorder)
                Resize(Capacity * 2);

            var entriesIndex = GetEntriesIndex(key);

            // Есть ли уже с таким хешем сущность
            if (_entries[entriesIndex] != null)
                _entries[entriesIndex].AddToList(new Entry<TKey, TValue>(key, value));
            else
                _entries[entriesIndex] = new Entry<TKey, TValue>(key, value);
        }

        /// <summary>
        ///     Удаление значения
        /// </summary>
        public void Remove(TKey key)
        {
            if (!IsExistsKey(key))
                throw new ArgumentException("Element with this key is not exists in hash table");
            
            if (Fullness < DownFullnessBorder && Capacity > EntriesMinSize)
                Resize(Capacity / 2);

            var entriesIndex = GetEntriesIndex(key);

            _entries[entriesIndex] = _entries[entriesIndex].RemoveFromList(key);
        }

        /// <summary>
        ///     True, если в entries уже есть значение с таким ключем
        /// </summary>
        bool IsExistsKey(TKey key)
        {
            foreach (var entry in _entries.Where(e => e != null))
                if (entry.Find(key) != null)
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

        /// <summary>
        ///     Ресайз массива _entries для того, чтобы:
        ///         1) Не пустовала большая часть массива (значений слишком мало, уменьшаем размер массива)
        ///         2) Не было коллизий из-за переполнения массива (значений слишком много, увеличиваем размер массива)
        /// </summary>
        void Resize(int newLength)
        {
            var bufferEntries = _entries.Where(e => e != null).SelectMany(e => e.ToEnumerable());
            _entries = new Entry<TKey, TValue>[newLength];
            foreach (var entry in bufferEntries)
                Insert(entry._key, entry._value);
        }
    }
}
