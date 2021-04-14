using System.Collections.Generic;
using System.Linq;

namespace Cormen.Core.Algorithms.Sorting
{
    public class InsertionSortClass
    {
        public static IEnumerable<T> Sort<T>(IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(x => x).ToList();
        }
    }
}
