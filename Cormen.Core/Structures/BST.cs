using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Core.Structures
{
    // Простое бинарное дерево поиска
    public class BST<TKey, TValue> where TKey : IComparable<TKey>
    {
        protected class BSTNode
        {
            internal TKey _key;
            internal TValue _value;
            internal BSTNode _left;
            internal BSTNode _right;
        }

        protected BSTNode _root;

        public List<TValue> IncoderTreeWalk()
        {
            return IncoderTreeWalk(_root, new List<TValue>());
        }

        List<TValue> IncoderTreeWalk(BSTNode node, List<TValue> result)
        {
            result ??= new List<TValue>();
            IncoderTreeWalk(node._left, result);
            result.Add(node._value);
            IncoderTreeWalk(node._right, result);
            return result;
        }

        BSTNode TreeSearch(BSTNode currentNode, TKey key)
        {
            if (currentNode == null)
                throw new ArgumentException("Node with this key is not exists");

            if (key.Equals(currentNode._key))
                return currentNode;

            var compareResult = key.CompareTo(currentNode._key);
            if (compareResult < 0)
                return TreeSearch(currentNode._left, currentNode._key);
            else
                return TreeSearch(currentNode._right, currentNode._key);
        }

        BSTNode TreeMinimum(BSTNode currentNode)
        {
            if (currentNode._left == null)
                return currentNode;
            return TreeMinimum(currentNode._left);
        }

        BSTNode TreeMaximum(BSTNode currentNode)
        {
            if (currentNode._right == null)
                return currentNode;
            return TreeMaximum(currentNode._right);
        }

        void DeleteNode(BSTNode node)
        {
            if (node._left != null)
                DeleteNode(node._left);
            else if (node._right != null)
                DeleteNode(node._right);
            else if (node != null)
            {
                node = null;
            }
        }
    }
}
