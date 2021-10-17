namespace CLRS.Core.Structures.Interfaces
{
    public interface IBinaryTreeNode<TKey, TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
        IBinaryTreeNode<TKey, TValue> Left { get; }
        IBinaryTreeNode<TKey, TValue> Right { get; }
    }
}
