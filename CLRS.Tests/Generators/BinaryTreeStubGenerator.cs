using System;
using CLRS.Tests.Stubs;

namespace CLRS.Tests.Generators
{
    /// <summary>
    ///     Генератор сущности <see cref="BinaryTreeNodeStub{TKey,TValue}"/>
    /// </summary>
    public static class BinaryTreeStubGenerator
    {
        public static BinaryTreeNodeStub<TKey, TValue> New<TKey, TValue>(TKey key, TValue value)
            where TKey : class, IComparable<TKey>
        {
            return new BinaryTreeNodeStub<TKey, TValue>(key, value);
        }
        
        public static BinaryTreeNodeStub<TKey, TValue> WithLeftNode<TKey, TValue>(this BinaryTreeNodeStub<TKey, TValue> node, 
                                                                                  TKey key, 
                                                                                  TValue value)
            where TKey : class, IComparable<TKey>
        {
            node.SetLeftNode(key, value);
            return node;
        }
        
        public static BinaryTreeNodeStub<TKey, TValue> WithRightNode<TKey, TValue>(this BinaryTreeNodeStub<TKey, TValue> node, 
                                                                                  TKey key, 
                                                                                  TValue value)
            where TKey : class, IComparable<TKey>
        {
            node.SetRightNode(key, value);
            return node;
        }
        
        public static BinaryTreeNodeStub<TKey, TValue> ToLeftNode<TKey, TValue>(this BinaryTreeNodeStub<TKey, TValue> node)
            where TKey : class, IComparable<TKey>
        {
            return (node.Left as BinaryTreeNodeStub<TKey, TValue>);
        }
        
        public static BinaryTreeNodeStub<TKey, TValue> ToRightNode<TKey, TValue>(this BinaryTreeNodeStub<TKey, TValue> node)
            where TKey : class, IComparable<TKey>
        {
            return (node.Right as BinaryTreeNodeStub<TKey, TValue>);
        }
    }
}
