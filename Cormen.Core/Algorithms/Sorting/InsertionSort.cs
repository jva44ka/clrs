using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cormen.Core.Algorithms.Sorting
{
    public static class InsertionSortClass
    {
        public static IEnumerable<T> InsertionSort<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(x => x).ToList();
        }
    }
}
