using System;
using System.Collections.Generic;

namespace CLRS.Core.Structures
{
    public class BSTNode<TKey, TValue> : IBinaryTreeNode<TKey, TValue>
        where TKey : class, IComparable<TKey>
    {
        internal TKey _key;
        internal TValue _value;
        internal BSTNode<TKey, TValue> _parent;
        internal BSTNode<TKey, TValue> _left;
        internal BSTNode<TKey, TValue> _right;

        public TKey Key => _key;
        public TValue Value => _value;
        public IBinaryTreeNode<TKey, TValue> Left => _left;
        public IBinaryTreeNode<TKey, TValue> Right => _right;

        public BSTNode()
        { }

        public BSTNode(BSTNode<TKey, TValue> parent, TKey key, TValue value)
        {
            _parent = parent;
            _key = key;
            _value = value;
        }

        public BSTNode<TKey, TValue> Search(TKey key)
        {
            //TODO: Возможно убрать дополнительное сравнение т.к. ниже есть вызов CompareTo
            if (key.Equals(_key))
                return this;

            var compareResult = key.CompareTo(_key);
            if (compareResult < 0)
                return _left?.Search(key);
            return _right?.Search(key);

        }

        /// <summary>
        ///     Центрированный обход дерева (LNR)
        /// </summary>
        /// <param name="result">Список, используемый для хранения результата обхода</param>
        /// <returns>Возвращает отсортированный по ключу в неубывающем порядке список всех узлов</returns>
        public List<TValue> InOrderTreeWalk(List<TValue> result = null)
        {
            result ??= new List<TValue>();
            
            _left?.InOrderTreeWalk(result);
            result.Add(_value);
            _right?.InOrderTreeWalk(result);

            return result;
        }

        /// <summary>
        ///     Поиск рекурсивно самого левого поддерева
        /// </summary>
        public BSTNode<TKey, TValue> Minimum()
        {
            if (_left == null)
                return this;
            return _left.Minimum();
        }

        /// <summary>
        ///     Поиск рекурсивно самого правого поддерева
        /// </summary>
        public BSTNode<TKey, TValue> Maximum()
        {
            if (_right == null)
                return this;
            return _right.Maximum();
        }

        /// <summary>
        ///     Вставка нового узла
        /// </summary>
        internal void Insert(TKey key, TValue value)
        {
            var compareResult = key.CompareTo(_key);
            if (compareResult < 0)
            {
                if (_left == null)
                    _left = new BSTNode<TKey, TValue>(this, key, value);
                else
                    _left.Insert(key, value);
            }
            else if (compareResult > 0)
            {
                if (_right == null)
                    _right = new BSTNode<TKey, TValue>(this, key, value);
                else
                    _right.Insert(key, value);
            }
            else
                throw new ArgumentException("This key already in the tree!");
        }

        /// <summary>
        ///     Удаляет узел из дерева
        /// </summary>
        /// <returns>Возвращает новый корень, если ключ удаляемого узла совпадает с текущим ключем</returns>
        internal BSTNode<TKey, TValue> Delete()
        {
            BSTNode<TKey, TValue> newRoot = null;
            BSTNode<TKey, TValue> someNode = null;

            if (_left == null)
                newRoot = Transplant(this, _right);
            else if (_right == null)
                newRoot = Transplant(this, _left);
            else
            {
                someNode = _right.Minimum();
                // TODO: Опасное сравнение нод через ==
                if (someNode._parent != this)
                {
                    newRoot = Transplant(someNode, someNode._right);
                    someNode._right = _right;
                    someNode._right._parent = someNode;
                }
                newRoot = Transplant(this, someNode);
                someNode._left = _left;
                someNode._left._parent = someNode;
            }

            return newRoot;

            /*if (this._key.CompareTo(key) == 0)
            {
                this._left._parent = this;
                this._right._parent = this;

                return this;
            }
            return null;*/
        }

        /// <summary>
        ///     Инвертирование дерева
        /// </summary>
        internal void Reverse()
        {
            _left?.Reverse();
            _right?.Reverse();

            // Обмен левой и правой ноды
            var bufferRight = _right;
            _right = _left;
            _left = bufferRight;
        }

        /// <summary>
        ///     Заменяет одно дерево, которое является дочерним по отношению
        ///     к своему родителю, другим поддеревом
        /// </summary>
        /// <returns>Возвращает новый корень если удаляемый узел был корневым</returns>
        BSTNode<TKey, TValue> Transplant(BSTNode<TKey, TValue> oldNode, BSTNode<TKey, TValue> newNode)
        {
            BSTNode<TKey, TValue> newRoot = null;
            if (oldNode._parent == null)
            {
                // У удаляемой ноды не было родителя значит она была корневой
                // и значит новая нода тоже корневая соответственно возвращаем ее как корневую
                newRoot = newNode;
            }
            else if (oldNode == oldNode._parent._left)
                oldNode._parent._left = newNode;
            else
                oldNode._parent._right = newNode;

            if (newNode != null) 
                newNode._parent = oldNode._parent;
            return newRoot;
        }
    }

    /// <summary>
    ///     Простое бинарное дерево поиска (Binary Search Tree | BST)
    /// </summary>
    public class BST<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        private BSTNode<TKey, TValue> _root;

        public BSTNode<TKey, TValue> Root => _root;

        public object this[TKey key]
        {
            get
            {
                var findedNode = _root.Search(key);
                if (findedNode != null)
                    return findedNode._value;
                return null;
            }
        }

        public List<TValue> InOrderTreeWalk()
        {
            return _root.InOrderTreeWalk(new List<TValue>());
        }

        public void Insert(TKey key, TValue value)
        {
            if (_root == null)
                _root = new BSTNode<TKey, TValue>(null, key, value);
            else
                _root.Insert(key, value);
        }

        public void Delete(TKey key)
        {
            var findedNode = _root.Search(key);
            if (findedNode == null)
                throw new ArgumentException("Node with this key not found");

            var newRoot = findedNode.Delete();
            if (newRoot != null) 
                _root = newRoot;
        }

        public void Reverse()
        {
            _root.Reverse();
        }
    }
}
