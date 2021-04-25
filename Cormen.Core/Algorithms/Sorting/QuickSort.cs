using Cormen.Core.Algorithms.Sorting.Interfaces;
using System.Collections.Generic;

namespace Cormen.Core.Algorithms.Sorting
{
    public class QuickSort : ISortable
    {
        public IList<int> Sort(IList<int> enumerable)
        {
            QuickSortAsc(enumerable, 0, enumerable.Count - 1);
            return enumerable;
        }

        public IList<int> SortDesc(IList<int> enumerable)
        {
            QuickSortDesc(enumerable, 0, enumerable.Count - 1);
            return enumerable;
        }

        private void QuickSortAsc(IList<int> enumerable, int p, int r)
        {
            if (p < r)
            {
                var q = PartitionAsc(enumerable, p, r);
                QuickSortAsc(enumerable, p, q - 1);
                QuickSortAsc(enumerable, q + 1, r);
            }
        }

        private int PartitionAsc(IList<int> enumerable, int p, int r)
        {
            var i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (enumerable[j] <= enumerable[r])
                {
                    i++;
                    Swap(enumerable, i, j);
                }
            }
            Swap(enumerable, i + 1, r);
            return i + 1;
        }

        private void QuickSortDesc(IList<int> enumerable, int p, int r)
        {
            if (p < r)
            {
                var q = PartitionDesc(enumerable, p, r);
                QuickSortDesc(enumerable, p, q - 1);
                QuickSortDesc(enumerable, q + 1, r);
            }
        }

        private int PartitionDesc(IList<int> enumerable, int p, int r)
        {
            var i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (enumerable[j] >= enumerable[r])
                {
                    i++;
                    Swap(enumerable, i, j);
                }
            }
            Swap(enumerable, i + 1, r);
            return i + 1;
        }

        private void Swap(IList<int> enumerable, int elIndex1, int elIndex2)
        {
            int buffer = enumerable[elIndex1];
            enumerable[elIndex1] = enumerable[elIndex2];
            enumerable[elIndex2] = buffer;
        } 
    }
}
