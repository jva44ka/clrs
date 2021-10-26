using System;
using System.Collections.Generic;

namespace CLRS.Core.Structures
{
    public class BTreeNode<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        int _keysCount = 0;                                 // x.n - количество ключей
        List<TKey> _keys = new List<TKey>();                // список x.n ключей в неубывающем порядке
        List<BTreeNode<TKey, TValue>> _subTrees;            // список x.(n + 1) указателей на дочерние узлы. У листьев этот список пуст
        bool _isLeaf = false;                               // True, если является листом. Листы содержат информацию (values)

        public BTreeNode()
        {
            _keysCount = 0;
            _keys = new List<TKey>();
            _subTrees = new List<BTreeNode<TKey, TValue>>();
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
                return _subTrees[i].Search(key);
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

    public class BTree<TKey, TValue>
    {

    }
}