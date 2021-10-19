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
        public static BinaryTreeNodeStub<TKey, TValue> Nil { get; } = new BinaryTreeNodeStub<TKey, TValue>();

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
        public void SetLeftNode(BinaryTreeNodeStub<TKey, TValue> node)
        {
            this._left = node;
        }
        
        public void SetRightNode(TKey key, TValue value)
        {
            this._right = new BinaryTreeNodeStub<TKey, TValue>(key, value);
        }
        
        
        public void SetRightNode(BinaryTreeNodeStub<TKey, TValue> node)
        {
            this._right = node;
        }

        // TODO: Неудобно дебажить, нет вывода отличий в консоль при падении теста
        // TODO: Для красно-черных деревьев не сравнивает цвет.
        // TODO: Также для них не сравнивает факт одного и того же объекта во всех Nil-листьях
        public bool Equals(IBinaryTreeNode<TKey, TValue> compareNode)
        {
            //TODO: Отрефакторить это место с учетом того, что ключ и значение могут быть пустые в Nil-нодах для RBT
            // Сравниваем текущую ноду
            if (_key == null && compareNode.Key != null 
                || _key != null && compareNode == null 
                || (!_key?.Equals(compareNode.Key) ?? false)) 
                return false;
            if (_value == null && compareNode.Value != null 
                || _value != null && compareNode.Value == null 
                || (!_value?.Equals(compareNode.Value) ?? false))
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
