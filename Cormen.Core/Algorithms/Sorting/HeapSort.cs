using Cormen.Core.Algorithms.Sorting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Core.Algorithms.Sorting
{
    public class HeapSort : ISort
    {
        #region ascending
        public IEnumerable<int> Sort(IList<int> enumerable)
        {
            return HeapSortFunc(enumerable, enumerable.Count);
        }

        int Parent(int i)
        {
            return (i - 1) / 2;
        }

        int Left(int i)
        {
            return 2 * i + 1;
        }

        int Right(int i)
        {
            return 2 * i + 2;
        }

        //Поддержание свойста невозрастающей пирамиды
        void MaxHepify(IList<int> enumerable, int heapSize, int i)
        {
            int l = Left(i);
            int r = Right(i);
            int largest;

            if (l <= (heapSize - 1) && enumerable[l] > enumerable[i])
                largest = l;
            else
                largest = i;

            if (r <= (heapSize - 1) && enumerable[r] > enumerable[largest])
                largest = r;

            if (largest != i)
            {
                SwapArrayElements(enumerable, i, largest);
                MaxHepify(enumerable, heapSize, largest);
            }
        }

        void BuildMaxHeap(IList<int> enumerable, int heapSize)
        {
            for (int i = heapSize / 2 - 1; i >= 0; i--)
                MaxHepify(enumerable, heapSize, i);

        }

        IList<int> HeapSortFunc(IList<int> enumerable, int heapSize)
        {
            BuildMaxHeap(enumerable, heapSize);
            for (int i = heapSize - 1; i > 0; i--)
            {
                SwapArrayElements(enumerable, 0, i);
                MaxHepify(enumerable, --heapSize, 0);
            }
            return enumerable;
        }

        // TODO Сделать обмен элементами без буфера
        private void SwapArrayElements(IList<int> list, int elIndex1, int elIndex2)
        {
            int buffer = list[elIndex1];
            list[elIndex1] = list[elIndex2];
            list[elIndex2] = buffer;
        }
        #endregion

        #region descending
        public IEnumerable<int> SortDesc(IList<int> enumerable)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
