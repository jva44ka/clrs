using System;
using System.Collections.Generic;

namespace Cormen.Core.Structures
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
    }
}
