using Cormen.Core.Algorithms.Sorting.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Cormen.Core.Algorithms.Sorting
{
    // Сортировка вставками - O(n ^ 2)
    public class InsertionSort : ISortable
    {
        public IList<int> Sort(IList<int> enumerable)
        {
            if (enumerable.Count <= 1)
                return enumerable;

            int key;
            int i;

            for (int j = 1; j < enumerable.Count(); j++)
            {
                key = enumerable[j];
                i = j - 1;

                while (i >= 0 && enumerable[i] > key)
                {
                    enumerable[i + 1] = enumerable[i];
                    i--;
                }
                enumerable[i + 1] = key;
            }
            return enumerable;
        }

        // По невозрастанию (убыванию)
        public IList<int> SortDesc(IList<int> enumerable)
        {
            if (enumerable.Count <= 1)
                return enumerable;

            int key;
            int i;

            for (int j = 1; j < enumerable.Count(); j++)
            {
                key = enumerable[j];
                i = j - 1;

                while (i >= 0 && enumerable[i] < key)
                {
                    enumerable[i + 1] = enumerable[i];
                    i--;
                }
                enumerable[i + 1] = key;
            }
            return enumerable;
        }
    }
}
