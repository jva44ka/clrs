using Cormen.Core.Algorithms.Sorting.Interfaces;
using System.Collections.Generic;

namespace Cormen.Core.Algorithms.Sorting
{
    // Сортировка кучей / пирамидальная - O(n log n)
    public class HeapSort : ISortable
    {
        public IList<int> Sort(IList<int> enumerable)
        {
            return HeapSortByAsc(enumerable, enumerable.Count);
        }

        public IList<int> SortDesc(IList<int> enumerable)
        {
            return HeapSortByDesc(enumerable, enumerable.Count);
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

        //Поддержание свойста неубывающей пирамиды
        void MinHepify(IList<int> enumerable, int heapSize, int i)
        {
            int l = Left(i);
            int r = Right(i);
            int lowest;

            if (l <= (heapSize - 1) && enumerable[l] < enumerable[i])
                lowest = l;
            else
                lowest = i;

            if (r <= (heapSize - 1) && enumerable[r] < enumerable[lowest])
                lowest = r;

            if (lowest != i)
            {
                SwapArrayElements(enumerable, i, lowest);
                MinHepify(enumerable, heapSize, lowest);
            }
        }

        void BuildMaxHeap(IList<int> enumerable, int heapSize)
        {
            for (int i = heapSize / 2 - 1; i >= 0; i--)
                MaxHepify(enumerable, heapSize, i);
        }

        void BuildMinHeap(IList<int> enumerable, int heapSize)
        {
            for (int i = heapSize / 2 - 1; i >= 0; i--)
                MinHepify(enumerable, heapSize, i);
        }

        IList<int> HeapSortByAsc(IList<int> enumerable, int heapSize)
        {
            BuildMaxHeap(enumerable, heapSize);
            for (int i = heapSize - 1; i > 0; i--)
            {
                SwapArrayElements(enumerable, 0, i);
                MaxHepify(enumerable, --heapSize, 0);
            }
            return enumerable;
        }

        IList<int> HeapSortByDesc(IList<int> enumerable, int heapSize)
        {
            BuildMinHeap(enumerable, heapSize);
            for (int i = heapSize - 1; i > 0; i--)
            {
                SwapArrayElements(enumerable, 0, i);
                MinHepify(enumerable, --heapSize, 0);
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
    }
}
