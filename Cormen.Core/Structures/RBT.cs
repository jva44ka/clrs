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
            if (node.left != null)
                IncoderTreeWalk(node.left, result);

            result.Add(node.value);
            if (node.right != null)
                IncoderTreeWalk(node.right, result);

            return result;
        }
    }

    public class RBT<TKey, TValue> where TKey : class, IComparable<TKey>
    {
        public RBTNode<TKey, TValue> root;
    }
}
