using Cormen.Core.Algorithms.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Core.Algorithms.Sorting
{
    // Сортировка слиянием - O(n log n)
    public class MergeSort : ISort
    {
        #region ascending
        public IEnumerable<int> Sort(IList<int> enumerable)
        {
            return MergeSortFunc(enumerable, 0, enumerable.Count - 1);
        }

        private IEnumerable<int> MergeSortFunc(IList<int> enumerable, int p, int r)
        {
            if (p < r)
            {
                int q = Math.Abs((p + r) / 2);
                MergeSortFunc(enumerable, p, q);
                MergeSortFunc(enumerable, q + 1, r);
                Merge(enumerable, p, q, r);
            }
            return enumerable;
        }

        private void Merge(IList<int> enumerable, int p, int q, int r)
        {
            int left = p;
            int right = q + 1;
            int[] tmp = new int[r - p + 1];
            int tmpIndex = 0;

            while ((left <= q) && (right <= r))
            {
                if (enumerable[left] < enumerable[right])
                {
                    tmp[tmpIndex] = enumerable[left];
                    left = left + 1;
                }
                else
                {
                    tmp[tmpIndex] = enumerable[right];
                    right = right + 1;
                }
                tmpIndex = tmpIndex + 1;
            }

            if (left <= q)
            {
                while (left <= q)
                {
                    tmp[tmpIndex] = enumerable[left];
                    left = left + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            if (right <= r)
            {
                while (right <= r)
                {
                    tmp[tmpIndex] = enumerable[right];
                    right = right + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                enumerable[p + i] = tmp[i];
            }
        }
        #endregion

        #region descending
        public IEnumerable<int> SortDesc(IList<int> enumerable)
        {
            return MergeSortFunc_Desc(enumerable, 0, enumerable.Count - 1);
        }

        private IEnumerable<int> MergeSortFunc_Desc(IList<int> enumerable, int p, int r)
        {
            if (p < r)
            {
                int q = Math.Abs((p + r) / 2);
                MergeSortFunc_Desc(enumerable, p, q);
                MergeSortFunc_Desc(enumerable, q + 1, r);
                MergeDesc(enumerable, p, q, r);
            }
            return enumerable;
        }

        private void MergeDesc(IList<int> enumerable, int p, int q, int r)
        {
            int left = p;
            int right = q + 1;
            int[] tmp = new int[r - p + 1];
            int tmpIndex = 0;

            while ((left <= q) && (right <= r))
            {
                if (enumerable[left] > enumerable[right])
                {
                    tmp[tmpIndex] = enumerable[left];
                    left = left + 1;
                }
                else
                {
                    tmp[tmpIndex] = enumerable[right];
                    right = right + 1;
                }
                tmpIndex = tmpIndex + 1;
            }

            if (left <= q)
            {
                while (left <= q)
                {
                    tmp[tmpIndex] = enumerable[left];
                    left = left + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            if (right <= r)
            {
                while (right <= r)
                {
                    tmp[tmpIndex] = enumerable[right];
                    right = right + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                enumerable[p + i] = tmp[i];
            }
        }
        #endregion
    }
}
