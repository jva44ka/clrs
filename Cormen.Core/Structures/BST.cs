using System;
using System.Collections.Generic;

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

        public object this[TKey key]
        {
            get
            {
                var findedNode = Search(_root, key);
                if (findedNode != null)
                    return findedNode._value;
                else
                    return null;
            }
        }

        public List<TValue> IncoderTreeWalk()
        {
            return IncoderTreeWalk(_root, new List<TValue>());
        }

        public void Insert(TKey key, TValue value)
        {
            Insert(ref _root, key, value);
        }

        public void Delete(TKey key)
        {
            var findedNode = Search(_root, key);
            Delete(findedNode);
        }

        List<TValue> IncoderTreeWalk(BSTNode node, List<TValue> result)
        {
            result ??= new List<TValue>();
            IncoderTreeWalk(node._left, result);
            result.Add(node._value);
            IncoderTreeWalk(node._right, result);
            return result;
        }

        BSTNode Search(BSTNode node, TKey key)
        {
            if (node == null)
                throw new ArgumentException("Node with this key is not exists");

            if (key.Equals(node._key))
                return node;

            var compareResult = key.CompareTo(node._key);
            if (compareResult < 0)
                return Search(node._left, node._key);
            else
                return Search(node._right, node._key);
        }

        void FindParent(BSTNode node, TKey key, ref BSTNode result)
        {
            if (node == null)
                result = null;

            if (key.Equals(node._left._key) || key.Equals(node._right._key))
                result = node;

            var compareResult = key.CompareTo(node._key);
            if (compareResult < 0)
                FindParent(node._left, node._key, ref result);
            else
                FindParent(node._right, node._key, ref result);
        }

        BSTNode Minimum(BSTNode currentNode)
        {
            if (currentNode._left == null)
                return currentNode;
            return Minimum(currentNode._left);
        }

        BSTNode TreeMaximum(BSTNode currentNode)
        {
            if (currentNode._right == null)
                return currentNode;
            return TreeMaximum(currentNode._right);
        }

        // Вставка узла
        void Insert(ref BSTNode node, TKey key, TValue value)
        {
            if (node == null)
                node = new BSTNode { _value = value };
            else
            {
                var compareResult = key.CompareTo(node._key);
                if (compareResult < 0)
                    Insert(ref node._left, key, value);
                else if (compareResult > 0)
                    Insert(ref node._right, key, value);
                else
                    throw new ArgumentException("This data already in the tree!");
            }
        }

        // Замена поддерева
        void Transplant(ref BSTNode oldNode, ref BSTNode newNode)
        {
            BSTNode oldNodeParent = null, newNodeParent = null;
            FindParent(_root, _root._key, ref oldNodeParent);
            FindParent(_root, _root._key, ref newNodeParent);

            if (oldNodeParent == null)
                _root = newNode;
            else if (oldNodeParent._left.Equals(oldNode))
                oldNodeParent._left = newNode;
            else
                oldNodeParent._right = newNode;

            if (newNode != null)
                newNodeParent = oldNodeParent;
        }

        // Удаление узла
        void Delete(BSTNode deletingNode)
        {
            if (deletingNode._left == null)
                Transplant(ref deletingNode, ref deletingNode._right);
            else if (deletingNode._right == null)
                Transplant(ref deletingNode, ref deletingNode._left);
            else
            {
                var minNode = Minimum(deletingNode._right);
                BSTNode minNodeParent = null;
                FindParent(minNode, minNode._key, ref minNodeParent);

                if (!minNodeParent.Equals(deletingNode))
                {
                    Transplant(ref minNode, ref minNode._right);
                    minNode._right = deletingNode._right;
                    BSTNode minNodeRightParent = null;
                    FindParent(minNode._right, minNode._key, ref minNodeRightParent);
                    minNodeRightParent = minNode;
                }

                Transplant(ref minNode, ref minNodeParent);
                minNode._left = deletingNode._left;
                BSTNode minNodeLeftParent = null;
                FindParent(minNode._right, minNode._key, ref minNodeLeftParent);
                minNodeLeftParent = minNode;
            }
        }
    }
}
