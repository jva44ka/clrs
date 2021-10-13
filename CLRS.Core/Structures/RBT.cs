using System;
using System.Collections.Generic;

namespace CLRS.Core.Structures
{
    public enum RBColor : byte
    {
        Red = 0,
        Black = 1
    }

    public class RBTNode<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        internal static RBTNode<TKey, TValue> nil = new RBTNode<TKey, TValue> { color = RBColor.Black };
        internal TKey key;
        internal TValue value;
        internal RBTNode<TKey, TValue> left;
        internal RBTNode<TKey, TValue> right;
        internal RBTNode<TKey, TValue> parent;
        internal RBColor color;

        internal int BlackHeight(RBTNode<TKey, TValue> node, int height)
        {
            if (node.color == RBColor.Black && node != RBTNode<TKey, TValue>.nil)
                height++;

            if (node.parent != null)
                return BlackHeight(node.parent, height);
            else
                return height;
        }

        internal RBTNode<TKey, TValue> Search(RBTNode<TKey, TValue> node, TKey key)
        {
            if (node == RBTNode<TKey, TValue>.nil)
                throw new ArgumentException("Node with this key is not exists");

            if (key.Equals(node.key))
                return node;

            var compareResult = key.CompareTo(node.key);
            if (compareResult < 0)
                return Search(node.left, key);
            else
                return Search(node.right, key);
        }

        internal RBTNode<TKey, TValue> Minimum(RBTNode<TKey, TValue> node)
        {
            if (node.left == RBTNode<TKey, TValue>.nil)
                return node;
            return Minimum(node.left);
        }

        internal RBTNode<TKey, TValue> Maximum(RBTNode<TKey, TValue> node)
        {
            if (node.right == RBTNode<TKey, TValue>.nil)
                return node;
            return Maximum(node.right);
        }

        internal List<TValue> IncoderTreeWalk(RBTNode<TKey, TValue> node, List<TValue> result)
        {
            result ??= new List<TValue>();
            if (node.left != RBTNode<TKey, TValue>.nil)
                IncoderTreeWalk(node.left, result);

            result.Add(node.value);
            if (node.right != RBTNode<TKey, TValue>.nil)
                IncoderTreeWalk(node.right, result);

            return result;
        }

        public override string ToString()
        {
            return key.ToString();
        }
    }

    public class RBT<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        public RBTNode<TKey, TValue> root;

        public RBT()
        {
            root = RBTNode<TKey, TValue>.nil;
        }

        public List<TValue> IncoderTreeWalk()
        {
            return root.IncoderTreeWalk(root, new List<TValue>());
        }

        public void Insert(TKey key, TValue value)
        {
            Insert(new RBTNode<TKey, TValue> 
            { 
                key = key,
                value = value
            });
        }

        public void Delete(TKey key)
        {
            var findedNode = root.Search(root, key);
            Delete(findedNode);
        }

        private RBTNode<TKey, TValue> LeftRotate(RBTNode<TKey, TValue> node)
        {
            // Сохранение правого поддерева node
            var newRoot = node.right;

            // Превращение левого поддерева newRoot в правое поддерево node
            node.right = newRoot.left;

            if (newRoot.left != RBTNode<TKey, TValue>.nil)
                newRoot.left.parent = node;

            // Передача родителя node узлу newRoot
            newRoot.parent = node.parent;
            if (node.parent == RBTNode<TKey, TValue>.nil)
                root = newRoot;
            else if (node == node.parent.left)
                node.parent.left = newRoot;
            else
                node.parent.right = newRoot;

            // Размещение node в качестве левого дочернего узла newRoot
            newRoot.left = node;

            node.parent = newRoot;

            return newRoot;
        }

        private RBTNode<TKey, TValue> RightRotate(RBTNode<TKey, TValue> node)
        {
            // Сохранение левого поддерева node
            var newRoot = node.left;

            // Превращение правого поддерева newRoot в левое поддерево node
            node.left = newRoot.right;

            if (newRoot.right != RBTNode<TKey, TValue>.nil)
                newRoot.right.parent = node;

            // Передача родителя node узлу newRoot
            newRoot.parent = node.parent;
            if (node.parent == RBTNode<TKey, TValue>.nil)
                root = newRoot;
            else if (node == node.parent.right)
                node.parent.right = newRoot;
            else
                node.parent.left = newRoot;

            // Размещение node в качестве правого дочернего узла newRoot
            newRoot.right = node;

            node.parent = newRoot;

            return newRoot;
        }

        private void Insert(RBTNode<TKey, TValue> newNode)
        {
            var y = RBTNode<TKey, TValue>.nil;
            var x = root;
            int compareResult;

            while (x != RBTNode<TKey, TValue>.nil)
            {
                y = x;
                compareResult = newNode.key.CompareTo(x.key);
                if (compareResult < 0)
                    x = x.left;
                else
                    x = x.right;
            }

            newNode.parent = y;
            compareResult = newNode.key.CompareTo(y.key);
            if (y == RBTNode<TKey, TValue>.nil)
                root = newNode;
            else if (compareResult < 0)
                y.left = newNode;
            else
                y.right = newNode;

            newNode.left = RBTNode<TKey, TValue>.nil;
            newNode.right = RBTNode<TKey, TValue>.nil;
            newNode.color = RBColor.Red;
            InsertFixup(newNode);
        }

        // Восстановление свойств красного дерева после вставки
        private void InsertFixup(RBTNode<TKey, TValue> newNode)
        {
            if (newNode.parent == RBTNode<TKey, TValue>.nil)
            {
                newNode.color = RBColor.Black;
                return;
            }
            else if (newNode.parent.parent == RBTNode<TKey, TValue>.nil)
                return;

            while (newNode.parent.color == RBColor.Red)
            {
                if (newNode.parent == newNode.parent.parent.left)
                {
                    var y = newNode.parent.parent.right;
                    if (y.color == RBColor.Red)
                    {
                        newNode.parent.color = RBColor.Black;       // Случай 1
                        y.color = RBColor.Black;                    // Случай 1
                        newNode.parent.parent.color = RBColor.Red;  // Случай 1
                        newNode = newNode.parent.parent;            // Случай 1
                    }
                    else
                    {
                        if (newNode == newNode.parent.right)
                        {
                            newNode = newNode.parent;               // Случай 2
                            LeftRotate(newNode);                    // Случай 2
                        }
                        newNode.parent.color = RBColor.Black;       // Случай 3
                        newNode.parent.parent.color = RBColor.Red;  // Случай 3
                        RightRotate(newNode.parent.parent);         // Случай 3
                    }
                }
                else
                {
                    var height = newNode.BlackHeight(newNode, 0);
                    var y = newNode.parent.parent.left;
                    if (y.color == RBColor.Red)
                    {
                        newNode.parent.color = RBColor.Black;       // Случай 4
                        y.color = RBColor.Black;                    // Случай 4
                        newNode.parent.parent.color = RBColor.Red;  // Случай 4
                        newNode = newNode.parent.parent;            // Случай 4
                    }
                    else
                    {
                        if (newNode == newNode.parent.left)
                        {
                            newNode = newNode.parent;               // Случай 5
                            RightRotate(newNode);                   // Случай 5
                        }
                        newNode.parent.color = RBColor.Black;       // Случай 6
                        newNode.parent.parent.color = RBColor.Red;  // Случай 6
                        LeftRotate(newNode.parent.parent);          // Случай 6
                    }
                }
            }

            root.color = RBColor.Black;
        }

        private void Transplant(RBTNode<TKey, TValue> oldNode, RBTNode<TKey, TValue> newNode)
        {
            if (oldNode.parent == RBTNode<TKey, TValue>.nil)
                root = newNode;
            else if (oldNode == oldNode.parent.left)
                oldNode.parent.left = newNode;
            else
                oldNode.parent.right = newNode;
            newNode.parent = oldNode.parent;
        }

        private void Delete(RBTNode<TKey, TValue> deletingNode)
        {
            var y = deletingNode;
            var yOriginalColor = y.color;
            RBTNode<TKey, TValue> x;

            if (deletingNode.left == RBTNode<TKey, TValue>.nil)
            {
                x = deletingNode.right;
                Transplant(deletingNode, deletingNode.right);
            }
            else if (deletingNode.right == RBTNode<TKey, TValue>.nil)
            {
                x = deletingNode.left;
                Transplant(deletingNode, deletingNode.left);
            }
            else
            {
                y = root.Minimum(root);
                x = y.right;
                if (y.parent == deletingNode)
                    x.parent = y;
                else
                {
                    Transplant(y, y.right);
                    y.right = deletingNode.right;
                    y.right.parent = y;
                }
                Transplant(deletingNode, y);
                y.left = deletingNode.left;
                y.left.parent = y;
                y.color = deletingNode.color;
            }

            if (yOriginalColor == RBColor.Black)
                DeleteFixup(x);

        }

        private void DeleteFixup(RBTNode<TKey, TValue> node)
        {
            RBTNode<TKey, TValue> w = RBTNode<TKey, TValue>.nil;

            while (node != root && node.color == RBColor.Black)
            {
                if (node == node.parent.left)
                {
                    w = node.parent.right;
                    if (w.color == RBColor.Red)
                    {
                        w.color = RBColor.Black;            // Случай 1
                        node.parent.color = RBColor.Red;    // Случай 1
                        LeftRotate(node.parent);            // Случай 1
                        w = node.parent.right;              // Случай 1
                    }
                    if (w.left.color == RBColor.Black && w.right.color == RBColor.Black)
                    {
                        w.color = RBColor.Red;              // Случай 2
                        node = node.parent;                 // Случай 2
                    }
                    else
                    {
                        if (w.right.color == RBColor.Black)
                        {
                            w.left.color = RBColor.Black;   // Случай 3
                            w.color = RBColor.Red;          // Случай 3
                            RightRotate(w);                 // Случай 3
                            w = node.parent.right;          // Случай 3
                        }
                        w.color = node.parent.color;        // Случай 4
                        node.parent.color = RBColor.Black;  // Случай 4
                        w.right.color = RBColor.Black;      // Случай 4
                        LeftRotate(node.parent);            // Случай 4
                        node = root;                        // Случай 4
                    }
                }
                else
                {
                    w = node.parent.left;
                    if (w.color == RBColor.Red)
                    {
                        w.color = RBColor.Black;            // Случай 5
                        node.parent.color = RBColor.Red;    // Случай 5
                        RightRotate(node.parent);           // Случай 5
                        w = node.parent.left;               // Случай 5
                    }
                    if (w.right.color == RBColor.Black && w.left.color == RBColor.Black)
                    {
                        w.color = RBColor.Red;              // Случай 6
                        node = node.parent;                 // Случай 6
                    }
                    else
                    {
                        if (w.left.color == RBColor.Black)
                        {
                            w.right.color = RBColor.Black;  // Случай 7
                            w.color = RBColor.Red;          // Случай 7
                            LeftRotate(w);                  // Случай 7
                            w = node.parent.left;           // Случай 7
                        }
                        w.color = node.parent.color;        // Случай 8
                        node.parent.color = RBColor.Black;  // Случай 8
                        w.left.color = RBColor.Black;       // Случай 8
                        RightRotate(node.parent);           // Случай 8
                        node = root;                        // Случай 8
                    }
                }
            }

            node.color = RBColor.Black;
        }
    }
}
