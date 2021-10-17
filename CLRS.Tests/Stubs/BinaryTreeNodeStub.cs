using System;
using CLRS.Core.Structures;

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
            if (!compareNode.Key.Equals(_key)) 
                return false;
            if (!compareNode.Value.Equals(_value)) 
                return false;

            // Если сравнение успешно идем рекурсивно вниз
            if (!compareNode.Left.Equals(_left))
                return false;
            if (!compareNode.Right.Equals(_right))
                return false;

            return true;
        }
    }
}
