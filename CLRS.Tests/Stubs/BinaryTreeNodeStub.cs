using System;
using CLRS.Core.Structures;
using CLRS.Core.Structures.Interfaces;

namespace CLRS.Tests.Stubs
{
    /// <summary>
    ///     Стаб для тестирования бинарных деревьев
    /// </summary>
    public class BinaryTreeNodeStub<TKey, TValue> : IBinaryTreeNode<TKey, TValue>
        where TKey : class, IComparable<TKey>
    {
        private TKey _key;
        private TValue _value;
        private BinaryTreeNodeStub<TKey, TValue> _left;
        private BinaryTreeNodeStub<TKey, TValue> _right;

        public TKey Key => _key;
        public TValue Value => _value;
        public IBinaryTreeNode<TKey, TValue> Left => _left;
        public IBinaryTreeNode<TKey, TValue> Right => _right;

        public BinaryTreeNodeStub()
        {}
        
        public BinaryTreeNodeStub(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public void SetLeftNode(TKey key, TValue value)
        {
            this._left = new BinaryTreeNodeStub<TKey, TValue>(key, value);
        }
        
        public void SetRightNode(TKey key, TValue value)
        {
            this._right = new BinaryTreeNodeStub<TKey, TValue>(key, value);
        }

        public bool Equals(IBinaryTreeNode<TKey, TValue> compareNode)
        {
            // Сравниваем текущую ноду
            if (!_key.Equals(compareNode.Key)) 
                return false;
            if (!_value.Equals(compareNode.Value)) 
                return false;

            //TODO: Отрефакторить это место с учетом того, что левая или правая поднода могут быть null
            //      При этом Equals нужно вызывать на объекте BinaryTreeNodeStub<>

            // Если сравнение успешно идем рекурсивно вниз
            if (_left == null && compareNode.Left != null
                || _left != null && compareNode.Left == null
                || (!_left?.Equals(compareNode.Left) ?? false))
                return false;
            if (_right == null && compareNode.Right != null
                || _right != null && compareNode.Right == null
                || (!_right?.Equals(compareNode.Right) ?? false))
                return false;

            return true;
        }

        public override bool Equals(object? obj)
        {
            if (obj is IBinaryTreeNode<TKey, TValue>)
                return Equals(obj as IBinaryTreeNode<TKey, TValue>);

            return base.Equals(obj);
        }
    }
}
