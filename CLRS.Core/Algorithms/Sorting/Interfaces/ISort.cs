using System.Collections.Generic;

namespace Cormen.Core.Algorithms.Sorting.Interfaces
{
    public interface ISortable
    {
        // По неубыванию (возрастанию)
        public IList<int> Sort(IList<int> enumerable);

        // По невозрастанию (убыванию)
        public IList<int> SortDesc(IList<int> enumerable);
    }
}
