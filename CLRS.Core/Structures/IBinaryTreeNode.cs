using System;
using System.Collections.Generic;
using System.Text;

namespace CLRS.Core.Structures
{
    interface IBinaryTreeNode<TNodeType>
    {
        TNodeType Left { get; }
        TNodeType Right { get; }
    }
}
