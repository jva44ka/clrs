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

        internal TKey _key;
        internal TValue _value;
        internal RBTNode<TKey, TValue> _left;
        internal RBTNode<TKey, TValue> _right;
        internal RBTNode<TKey, TValue> _parent;
        internal RBColor _color;

        public TKey Key => _key;
        public TValue Value => _value;
        public IBinaryTreeNode<TKey, TValue> Left => _left;
        public IBinaryTreeNode<TKey, TValue> Right => _right;

        public int BlackHeight(int height)
        {
            if (_color == RBColor.Black && this != Nil)
                height++;

            if (_parent != null)
                return _parent.BlackHeight(height);
            else
                return height;
        }

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

        internal RBTNode<TKey, TValue> Minimum()
        {
            if (_left == Nil)
                return this;
            return _left.Minimum();
        }

        internal RBTNode<TKey, TValue> Maximum()
        {
            if (_right == Nil)
                return this;
            return _right.Maximum();
        }

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
    }

    public class RBT<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        public RBTNode<TKey, TValue> _root;

        public RBTNode<TKey, TValue> Root => _root;

        public RBT()
        {
            _root = RBTNode<TKey, TValue>.Nil;
        }

        public List<TValue> InOrderTreeWalk()
        {
            return _root.InOrderTreeWalk(new List<TValue>());
        }

        public void Insert(TKey key, TValue value)
        {
            Insert(new RBTNode<TKey, TValue> 
            { 
                _key = key,
                _value = value
            });
        }

        public void Delete(TKey key)
        {
            var findedNode = _root.Search(key);
            Delete(findedNode);
        }

        private RBTNode<TKey, TValue> LeftRotate(RBTNode<TKey, TValue> node)
        {
            // Сохранение правого поддерева node
            var newRoot = node._right;

            // Превращение левого поддерева newRoot в правое поддерево node
            node._right = newRoot._left;

            if (newRoot._left != RBTNode<TKey, TValue>.Nil)
                newRoot._left._parent = node;

            // Передача родителя node узлу newRoot
            newRoot._parent = node._parent;
            if (node._parent == RBTNode<TKey, TValue>.Nil)
                _root = newRoot;
            else if (node == node._parent._left)
                node._parent._left = newRoot;
            else
                node._parent._right = newRoot;

            // Размещение node в качестве левого дочернего узла newRoot
            newRoot._left = node;

            node._parent = newRoot;

            return newRoot;
        }

        private RBTNode<TKey, TValue> RightRotate(RBTNode<TKey, TValue> node)
        {
            // Сохранение левого поддерева node
            var newRoot = node._left;

            // Превращение правого поддерева newRoot в левое поддерево node
            node._left = newRoot._right;

            if (newRoot._right != RBTNode<TKey, TValue>.Nil)
                newRoot._right._parent = node;

            // Передача родителя node узлу newRoot
            newRoot._parent = node._parent;
            if (node._parent == RBTNode<TKey, TValue>.Nil)
                _root = newRoot;
            else if (node == node._parent._right)
                node._parent._right = newRoot;
            else
                node._parent._left = newRoot;

            // Размещение node в качестве правого дочернего узла newRoot
            newRoot._right = node;

            node._parent = newRoot;

            return newRoot;
        }

        private void Insert(RBTNode<TKey, TValue> newNode)
        {
            var y = RBTNode<TKey, TValue>.Nil;
            var x = _root;
            int compareResult;

            while (x != RBTNode<TKey, TValue>.Nil)
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
            if (y == RBTNode<TKey, TValue>.Nil)
                _root = newNode;
            else if (compareResult < 0)
                y._left = newNode;
            else
                y._right = newNode;

            newNode._left = RBTNode<TKey, TValue>.Nil;
            newNode._right = RBTNode<TKey, TValue>.Nil;
            newNode._color = RBColor.Red;
            InsertFixup(newNode);
        }

        // Восстановление свойств красного дерева после вставки
        private void InsertFixup(RBTNode<TKey, TValue> newNode)
        {
            if (newNode._parent == RBTNode<TKey, TValue>.Nil)
            {
                newNode._color = RBColor.Black;
                return;
            }
            else if (newNode._parent._parent == RBTNode<TKey, TValue>.Nil)
                return;

            while (newNode._parent._color == RBColor.Red)
            {
                if (newNode._parent == newNode._parent._parent._left)
                {
                    var y = newNode._parent._parent._right;
                    if (y._color == RBColor.Red)
                    {
                        newNode._parent._color = RBColor.Black;       // Случай 1
                        y._color = RBColor.Black;                    // Случай 1
                        newNode._parent._parent._color = RBColor.Red;  // Случай 1
                        newNode = newNode._parent._parent;            // Случай 1
                    }
                    else
                    {
                        if (newNode == newNode._parent._right)
                        {
                            newNode = newNode._parent;               // Случай 2
                            LeftRotate(newNode);                    // Случай 2
                        }
                        newNode._parent._color = RBColor.Black;       // Случай 3
                        newNode._parent._parent._color = RBColor.Red;  // Случай 3
                        RightRotate(newNode._parent._parent);         // Случай 3
                    }
                }
                else
                {
                    var height = newNode.BlackHeight(0);
                    var y = newNode._parent._parent._left;
                    if (y._color == RBColor.Red)
                    {
                        newNode._parent._color = RBColor.Black;       // Случай 4
                        y._color = RBColor.Black;                    // Случай 4
                        newNode._parent._parent._color = RBColor.Red;  // Случай 4
                        newNode = newNode._parent._parent;            // Случай 4
                    }
                    else
                    {
                        if (newNode == newNode._parent._left)
                        {
                            newNode = newNode._parent;               // Случай 5
                            RightRotate(newNode);                   // Случай 5
                        }
                        newNode._parent._color = RBColor.Black;       // Случай 6
                        newNode._parent._parent._color = RBColor.Red;  // Случай 6
                        LeftRotate(newNode._parent._parent);          // Случай 6
                    }
                }
            }

            _root._color = RBColor.Black;
        }

        private void Transplant(RBTNode<TKey, TValue> oldNode, RBTNode<TKey, TValue> newNode)
        {
            if (oldNode._parent == RBTNode<TKey, TValue>.Nil)
                _root = newNode;
            else if (oldNode == oldNode._parent._left)
                oldNode._parent._left = newNode;
            else
                oldNode._parent._right = newNode;
            newNode._parent = oldNode._parent;
        }

        private void Delete(RBTNode<TKey, TValue> deletingNode)
        {
            var y = deletingNode;
            var yOriginalColor = y._color;
            RBTNode<TKey, TValue> x;

            if (deletingNode._left == RBTNode<TKey, TValue>.Nil)
            {
                x = deletingNode._right;
                Transplant(deletingNode, deletingNode._right);
            }
            else if (deletingNode._right == RBTNode<TKey, TValue>.Nil)
            {
                x = deletingNode._left;
                Transplant(deletingNode, deletingNode._left);
            }
            else
            {
                y = deletingNode._right.Minimum();
                yOriginalColor = y._color;
                x = y._right;
                if (y._parent == deletingNode)
                    x._parent = y;
                else
                {
                    Transplant(y, y._right);
                    y._right = deletingNode._right;
                    y._right._parent = y;
                }
                Transplant(deletingNode, y);
                y._left = deletingNode._left;
                y._left._parent = y;
                y._color = deletingNode._color;
            }

            if (yOriginalColor == RBColor.Black)
                DeleteFixup(x);

        }

        private void DeleteFixup(RBTNode<TKey, TValue> node)
        {
            RBTNode<TKey, TValue> w = RBTNode<TKey, TValue>.Nil;

            while (node != _root && node._color == RBColor.Black)
            {
                if (node == node._parent._left)
                {
                    w = node._parent._right;
                    if (w._color == RBColor.Red)
                    {
                        w._color = RBColor.Black;            // Случай 1
                        node._parent._color = RBColor.Red;    // Случай 1
                        LeftRotate(node._parent);            // Случай 1
                        w = node._parent._right;              // Случай 1
                    }
                    if (w._left._color == RBColor.Black && w._right._color == RBColor.Black)
                    {
                        w._color = RBColor.Red;              // Случай 2
                        node = node._parent;                 // Случай 2
                    }
                    else
                    {
                        if (w._right._color == RBColor.Black)
                        {
                            w._left._color = RBColor.Black;   // Случай 3
                            w._color = RBColor.Red;          // Случай 3
                            RightRotate(w);                 // Случай 3
                            w = node._parent._right;          // Случай 3
                        }
                        w._color = node._parent._color;        // Случай 4
                        node._parent._color = RBColor.Black;  // Случай 4
                        w._right._color = RBColor.Black;      // Случай 4
                        LeftRotate(node._parent);            // Случай 4
                        node = _root;                        // Случай 4
                    }
                }
                else
                {
                    w = node._parent._left;
                    if (w._color == RBColor.Red)
                    {
                        w._color = RBColor.Black;            // Случай 5
                        node._parent._color = RBColor.Red;    // Случай 5
                        RightRotate(node._parent);           // Случай 5
                        w = node._parent._left;               // Случай 5
                    }
                    if (w._right._color == RBColor.Black && w._left._color == RBColor.Black)
                    {
                        w._color = RBColor.Red;              // Случай 6
                        node = node._parent;                 // Случай 6
                    }
                    else
                    {
                        if (w._left._color == RBColor.Black)
                        {
                            w._right._color = RBColor.Black;  // Случай 7
                            w._color = RBColor.Red;          // Случай 7
                            LeftRotate(w);                  // Случай 7
                            w = node._parent._left;           // Случай 7
                        }
                        w._color = node._parent._color;        // Случай 8
                        node._parent._color = RBColor.Black;  // Случай 8
                        w._left._color = RBColor.Black;       // Случай 8
                        RightRotate(node._parent);           // Случай 8
                        node = _root;                        // Случай 8
                    }
                }
            }

            node._color = RBColor.Black;
        }
    }
}
