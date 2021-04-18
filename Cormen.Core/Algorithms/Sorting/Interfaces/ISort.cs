using System.Collections.Generic;

namespace Cormen.Core.Algorithms.Sorting.Interfaces
{
    public interface ISort
    {
        // По неубыванию (возрастанию)
        public IEnumerable<int> Sort(IList<int> enumerable);

        // По невозрастанию (убыванию)
        public IEnumerable<int> SortDesc(IList<int> enumerable);
    }
}
