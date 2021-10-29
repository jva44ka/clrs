using System;
using System.Collections.Generic;

namespace CLRS.Core.Structures
{
    public class BTreeNode<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        int _keysCount;                                     // x.n - количество ключей
        List<TKey> _keys = new List<TKey>();                // список x.n ключей в неубывающем порядке
        BTreeNode<TKey, TValue> _parent;                    // родительский узел
        BTreeNode<TKey, TValue> _child;                     // первый дорчерний узел из списка. У листьев этот список пуст
        BTreeNode<TKey, TValue> _next;                      // следующий за текущим узел на том же уровне (брат)
        bool _isLeaf;                                       // True, если является листом. Листы содержат информацию (values)

        public BTreeNode()
        {
            _keysCount = 0;
            _keys = new List<TKey>();
            _isLeaf = true;   
        }

        public BTreeSearchResult<TKey, TValue> Search(TKey key)
        {
            var i = 1;
            while (i < _keysCount && key.CompareTo(_keys[i]) > 0) 
                i++;
            if (i <= _keysCount && key.CompareTo(_keys[i]) == 0)
                return new BTreeSearchResult<TKey, TValue>(this, i);
            else if (_isLeaf)
                throw new ArgumentException("That value not found");
            else
                return _next.Search(key);
        }

        public class BTreeSearchResult<TKey, TValue> where TKey : class, IComparable<TKey>
        {
            /// <summary>
            ///     Узел
            /// </summary>
            BTreeNode<TKey, TValue> Node { get; }

            /// <summary>
            ///     Индекс i, при котором y.key(i) = k
            /// </summary>
            int Index { get; }

            public BTreeSearchResult(BTreeNode<TKey, TValue> node, int index)
            {
                Node = node;
                Index = index;
            }
        }
    }

    public class BTree<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        private int _t = 2;
        private BTreeNode<TKey, TValue> _root;

        public BTree()
        { }

        public BTree(int t)
        {
            _t = t;
        }
        
        public BTree(BTreeNode<TKey, TValue> root)
        {
            _root = root;
        }
        
        public BTree(int t, BTreeNode<TKey, TValue> root)
        {
            _t = t;
            _root = root;
        }


    }
}