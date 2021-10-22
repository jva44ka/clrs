using System;
using System.Collections.Generic;
using CLRS.Core.Structures.Interfaces;

namespace CLRS.Core.Structures
{
    public enum RBColor : byte
    {
        Red = 0,
        Black = 1
    }

    public class RBTNode<TKey, TValue> : IBinaryTreeNode<TKey, TValue>
        where TKey : class, IComparable<TKey>
    {
        public static RBTNode<TKey, TValue> Nil { get; } = new RBTNode<TKey, TValue> { _color = RBColor.Black };

        TKey _key;
        TValue _value;
        RBTNode<TKey, TValue> _left;
        RBTNode<TKey, TValue> _right;
        RBTNode<TKey, TValue> _parent;
        RBColor _color;

        public TKey Key => _key;
        public TValue Value => _value;
        public IBinaryTreeNode<TKey, TValue> Left => _left;
        public IBinaryTreeNode<TKey, TValue> Right => _right;

        public RBTNode()
        { }

        public RBTNode(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        #region public

        /// <summary>
        ///     Черная высота - количество черных узлов от текущей ноды до корня без учета Nil-узлов
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int BlackHeight(int height)
        {
            if (_color == RBColor.Black && this != Nil)
                height++;

            if (_parent != null)
                return _parent.BlackHeight(height);
            else
                return height;
        }

        /// <summary>
        ///     Поиск узла по ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RBTNode<TKey, TValue> Search(TKey key)
        {
            if (this == Nil)
                throw new ArgumentException("Node with this key is not exists");

            if (key.Equals(_key))
                return this;

            var compareResult = key.CompareTo(_key);
            if (compareResult < 0)
                return _left.Search(key);
            else
                return _right.Search(key);
        }

        /// <summary>
        ///     Поиск рекурсивно самого левого поддерева
        /// </summary>
        internal RBTNode<TKey, TValue> Minimum()
        {
            if (_left == Nil)
                return this;
            return _left.Minimum();
        }

        /// <summary>
        ///     Поиск рекурсивно самого правого поддерева
        /// </summary>
        internal RBTNode<TKey, TValue> Maximum()
        {
            if (_right == Nil)
                return this;
            return _right.Maximum();
        }

        /// <summary>
        ///     Центрированный обход дерева (LNR)
        /// </summary>
        /// <param name="result">Список, используемый для хранения результата обхода</param>
        /// <returns>Возвращает отсортированный по ключу в неубывающем порядке список всех узлов</returns>
        internal List<TValue> InOrderTreeWalk(List<TValue> result)
        {
            result ??= new List<TValue>();

            if (_left != Nil)
                _left.InOrderTreeWalk(result);
            result.Add(_value);
            if (_right != Nil)
                _right.InOrderTreeWalk(result);

            return result;
        }

        /// <summary>
        ///     Вставка нового узла
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="tree"></param>
        internal void Insert(RBTNode<TKey, TValue> newNode, RBT<TKey, TValue> tree)
        {
            var y = Nil;
            var x = tree._root;
            int compareResult;

            while (x != Nil)
            {
                y = x;
                compareResult = newNode._key.CompareTo(x._key);
                if (compareResult < 0)
                    x = x._left;
                else
                    x = x._right;
            }

            newNode._parent = y;
            compareResult = newNode._key.CompareTo(y._key);
            if (y == Nil)
                tree._root = newNode;
            else if (compareResult < 0)
                y._left = newNode;
            else
                y._right = newNode;

            newNode._left = Nil;
            newNode._right = Nil;
            newNode._color = RBColor.Red;
            InsertFixup(tree, newNode);
        }

        /// <summary>
        ///     Удаление узла из дерева
        /// </summary>
        /// <param name="tree"></param>
        internal void Delete(RBT<TKey, TValue> tree)
        {
            var y = this;
            var yOriginalColor = y._color;
            RBTNode<TKey, TValue> x;

            if (_left == Nil)
            {
                x = _right;
                Transplant(tree, this, _right);
            }
            else if (_right == Nil)
            {
                x = _left;
                Transplant(tree, this, _left);
            }
            else
            {
                y = _right.Minimum();
                yOriginalColor = y._color;
                x = y._right;
                if (y._parent == this)
                    x._parent = y;
                else
                {
                    Transplant(tree, y, y._right);
                    y._right = _right;
                    y._right._parent = y;
                }
                Transplant(tree, this, y);
                y._left = _left;
                y._left._parent = y;
                y._color = _color;
            }

            if (yOriginalColor == RBColor.Black)
                DeleteFixup(x, tree);

        }

        #endregion

        #region private

        /// <summary>
        ///     Левый поворот, при котором правое поддерево становится главенствующим
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private RBTNode<TKey, TValue> LeftRotate(RBT<TKey, TValue> tree)
        {
            // Сохранение правого поддерева node
            var rightSubTree = _right;

            // Превращение левого поддерева newRoot в правое поддерево this
            _right = rightSubTree._left;

            if (rightSubTree._left != Nil)
                rightSubTree._left._parent = this;

            // Передача родителя this узлу newRoot
            rightSubTree._parent = _parent;
            if (_parent == Nil)
                tree._root = rightSubTree;
            else if (this == _parent._left)
                _parent._left = rightSubTree;
            else
                _parent._right = rightSubTree;

            // Размещение this в качестве левого дочернего узла newRoot
            rightSubTree._left = this;

            _parent = rightSubTree;

            return rightSubTree;
        }

        /// <summary>
        ///     Правый поворот, при котором левое поддерево становится главенствующим
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        private RBTNode<TKey, TValue> RightRotate(RBT<TKey, TValue> tree)
        {
            // Сохранение левого поддерева this
            var leftSubTree = _left;

            // Превращение правого поддерева newRoot в левое поддерево this
            _left = leftSubTree._right;

            if (leftSubTree._right != Nil)
                leftSubTree._right._parent = this;

            // Передача родителя this узлу newRoot
            leftSubTree._parent = _parent;
            if (_parent == Nil)
                tree._root = leftSubTree;
            else if (this == _parent._right)
                _parent._right = leftSubTree;
            else
                _parent._left = leftSubTree;

            // Размещение this в качестве правого дочернего узла newRoot
            leftSubTree._right = this;

            _parent = leftSubTree;

            return leftSubTree;
        }

        /// <summary>
        ///     Восстановление свойств красного дерева после вставки (балансировка)
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="newNode"></param>
        private void InsertFixup(RBT<TKey, TValue> tree, RBTNode<TKey, TValue> newNode)
        {
            if (newNode._parent == Nil)
            {
                newNode._color = RBColor.Black;
                return;
            }
            else if (newNode._parent._parent == Nil)
                return;

            while (newNode._parent._color == RBColor.Red)
            {
                if (newNode._parent == newNode._parent._parent._left)
                {
                    var y = newNode._parent._parent._right;
                    if (y._color == RBColor.Red)
                    {
                        newNode._parent._color = RBColor.Black;         // Случай 1
                        y._color = RBColor.Black;                       // Случай 1
                        newNode._parent._parent._color = RBColor.Red;   // Случай 1
                        newNode = newNode._parent._parent;              // Случай 1
                    }
                    else
                    {
                        if (newNode == newNode._parent._right)
                        {
                            newNode = newNode._parent;                  // Случай 2
                            newNode.LeftRotate(tree);                   // Случай 2
                        }
                        newNode._parent._color = RBColor.Black;         // Случай 3
                        newNode._parent._parent._color = RBColor.Red;   // Случай 3
                        newNode._parent._parent.RightRotate(tree);      // Случай 3
                    }
                }
                else
                {
                    var y = newNode._parent._parent._left;
                    if (y._color == RBColor.Red)
                    {
                        newNode._parent._color = RBColor.Black;         // Случай 4
                        y._color = RBColor.Black;                       // Случай 4
                        newNode._parent._parent._color = RBColor.Red;   // Случай 4
                        newNode = newNode._parent._parent;              // Случай 4
                    }
                    else
                    {
                        if (newNode == newNode._parent._left)
                        {
                            newNode = newNode._parent;                  // Случай 5
                            newNode.RightRotate(tree);                  // Случай 5
                        }
                        newNode._parent._color = RBColor.Black;         // Случай 6
                        newNode._parent._parent._color = RBColor.Red;   // Случай 6
                        newNode._parent._parent.LeftRotate(tree);       // Случай 6
                    }
                }
            }

            tree._root._color = RBColor.Black;
        }

        /// <summary>
        ///     Замена одного поддерева другим
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="oldNode"></param>
        /// <param name="newNode"></param>
        private void Transplant(RBT<TKey, TValue> tree, 
            RBTNode<TKey, TValue> oldNode,
            RBTNode<TKey, TValue> newNode)
        {
            if (oldNode._parent == Nil)
                tree._root = newNode;
            else if (oldNode == oldNode._parent._left)
                oldNode._parent._left = newNode;
            else
                oldNode._parent._right = newNode;
            newNode._parent = oldNode._parent;
        }

        /// <summary>
        ///     Восстановление свойств красного дерева после удаления узла (балансировка)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tree"></param>
        private void DeleteFixup(RBTNode<TKey, TValue> node, RBT<TKey, TValue> tree)
        {
            RBTNode<TKey, TValue> w = Nil;

            while (node != tree._root && node._color == RBColor.Black)
            {
                if (node == node._parent._left)
                {
                    w = node._parent._right;
                    if (w._color == RBColor.Red)
                    {
                        w._color = RBColor.Black;               // Случай 1
                        node._parent._color = RBColor.Red;      // Случай 1
                        node._parent.LeftRotate(tree);          // Случай 1
                        w = node._parent._right;                // Случай 1
                    }
                    if (w._left._color == RBColor.Black && w._right._color == RBColor.Black)
                    {
                        w._color = RBColor.Red;                 // Случай 2
                        node = node._parent;                    // Случай 2
                    }
                    else
                    {
                        if (w._right._color == RBColor.Black)
                        {
                            w._left._color = RBColor.Black;     // Случай 3
                            w._color = RBColor.Red;             // Случай 3
                            w.RightRotate(tree);                // Случай 3
                            w = node._parent._right;            // Случай 3
                        }
                        w._color = node._parent._color;         // Случай 4
                        node._parent._color = RBColor.Black;    // Случай 4
                        w._right._color = RBColor.Black;        // Случай 4
                        node._parent.LeftRotate(tree);          // Случай 4
                        node = tree._root;                      // Случай 4
                    }
                }
                else
                {
                    w = node._parent._left;
                    if (w._color == RBColor.Red)
                    {
                        w._color = RBColor.Black;               // Случай 5
                        node._parent._color = RBColor.Red;      // Случай 5
                        node._parent.RightRotate(tree);         // Случай 5
                        w = node._parent._left;                 // Случай 5
                    }
                    if (w._right._color == RBColor.Black && w._left._color == RBColor.Black)
                    {
                        w._color = RBColor.Red;                 // Случай 6
                        node = node._parent;                    // Случай 6
                    }
                    else
                    {
                        if (w._left._color == RBColor.Black)
                        {
                            w._right._color = RBColor.Black;    // Случай 7
                            w._color = RBColor.Red;             // Случай 7
                            w.LeftRotate(tree);                 // Случай 7
                            w = node._parent._left;             // Случай 7
                        }
                        w._color = node._parent._color;         // Случай 8
                        node._parent._color = RBColor.Black;    // Случай 8
                        w._left._color = RBColor.Black;         // Случай 8
                        node._parent.RightRotate(tree);         // Случай 8
                        node = tree._root;                      // Случай 8
                    }
                }
            }

            node._color = RBColor.Black;
        }

        #endregion
    }

    public class RBT<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        public RBTNode<TKey, TValue> _root;

        public RBTNode<TKey, TValue> Root => _root;

        public RBT()
        {
            _root = RBTNode<TKey, TValue>.Nil;
        }

        public object this[TKey key]
        {
            get
            {
                var findedNode = _root.Search(key);
                if (findedNode != null)
                    return findedNode.Value;
                return null;
            }
        }

        /// <summary>
        ///     Центрированный обход дерева (LNR)
        /// </summary>
        /// <returns>Возвращает отсортированный по ключу в неубывающем порядке список всех узлов</returns>
        public List<TValue> InOrderTreeWalk()
        {
            return _root.InOrderTreeWalk(new List<TValue>());
        }

        /// <summary>
        ///     Поиск рекурсивно самого левого поддерева
        /// </summary>
        public RBTNode<TKey, TValue> Minimum()
        {
            return _root.Minimum();
        }
        
        /// <summary>
        ///     Поиск рекурсивно самого правого поддерева
        /// </summary>
        public RBTNode<TKey, TValue> Maximum()
        {
            return _root.Maximum();
        }

        /// <summary>
        ///     Вставка нового узла в дерево
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Insert(TKey key, TValue value)
        {
            _root.Insert(new RBTNode<TKey, TValue>(key, value), this);
        }

        /// <summary>
        ///     Удаление узла из дерева
        /// </summary>
        /// <param name="key"></param>
        public void Delete(TKey key)
        {
            var findedNode = _root.Search(key);
            findedNode.Delete(this);
        }
    }
}
