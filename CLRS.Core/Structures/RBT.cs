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

        internal int BlackHeight(RBTNode<TKey, TValue> node, int height)
        {
            if (node._color == RBColor.Black && node != Nil)
                height++;

            if (node._parent != null)
                return BlackHeight(node._parent, height);
            else
                return height;
        }

        internal RBTNode<TKey, TValue> Search(RBTNode<TKey, TValue> node, TKey key)
        {
            if (node == Nil)
                throw new ArgumentException("Node with this key is not exists");

            if (key.Equals(node._key))
                return node;

            var compareResult = key.CompareTo(node._key);
            if (compareResult < 0)
                return Search(node._left, key);
            else
                return Search(node._right, key);
        }

        internal RBTNode<TKey, TValue> Minimum(RBTNode<TKey, TValue> node)
        {
            if (node._left == Nil)
                return node;
            return Minimum(node._left);
        }

        internal RBTNode<TKey, TValue> Maximum(RBTNode<TKey, TValue> node)
        {
            if (node._right == Nil)
                return node;
            return Maximum(node._right);
        }

        internal List<TValue> InOrderTreeWalk(RBTNode<TKey, TValue> node, List<TValue> result)
        {
            result ??= new List<TValue>();
            if (node._left != Nil)
                InOrderTreeWalk(node._left, result);

            result.Add(node._value);
            if (node._right != Nil)
                InOrderTreeWalk(node._right, result);

            return result;
        }

        public override string ToString()
        {
            return _key.ToString();
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
            return _root.InOrderTreeWalk(_root, new List<TValue>());
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
            var findedNode = _root.Search(_root, key);
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
                    var height = newNode.BlackHeight(newNode, 0);
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
                y = _root.Minimum(_root);
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
