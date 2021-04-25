using Cormen.Core.Algorithms.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Core.Algorithms.Sorting
{
    // Сортировка подсчетом - O(k + n)
    public class CountingSort : ISortable
    {
        public IList<int> Sort(IList<int> enumerable)
        {
            CountingSortAsc(enumerable, enumerable.Count);
            return enumerable;
        }

        public IList<int> SortDesc(IList<int> enumerable)
        {
            throw new NotImplementedException();
        }

        private void CountingSortAsc(IList<int> enumerable, int k)
        {
            var c = new int[k + 1];
            var b = new int[enumerable.Count];
            for (int i = 0; i <= k; i++)
                c[i] = 0;

            for (int j = 1; j < k; j++)
                c[enumerable[j]]++;

            for (int i = 1; i <= k; i++)
                c[i] = c[i] + c[i - 1];

            for (int j = enumerable.Count - 1; j >= 1; j--)
            {
                b[c[enumerable[j]]] = enumerable[j];
                c[enumerable[j]]--;
            }
        }
    }
}
