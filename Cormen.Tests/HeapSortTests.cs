using Cormen.Core.Algorithms.Sorting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cormen.Tests
{
    [TestFixture(Category = "Sorting")]
    public class HeapSortTests
    {
        private static List<List<int>> testData = new List<List<int>>
        {
            new List<int> {1, 2, 3, 4, 5, 6},
            new List<int> {1, 2, 2, 2, 5, 6},
            new List<int> {6, 5, 4, 3, 2, 1},
            new List<int> {1, 1, 1, 1, 1, 1},
            new List<int> {2, 2, 3},
            new List<int> {2, 3, 3},
        };

        [TestCaseSource(nameof(testData))]
        public void HeapSortInts(List<int> enumerable)
        {
            var sortedArray = enumerable.OrderBy(x => x).ToArray();
            var arrayToCheck = new HeapSort().Sort(enumerable);
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }

        [TestCaseSource(nameof(testData))]
        public void HeapSortInts_Desc(List<int> enumerable)
        {
            var sortedArray = enumerable.OrderByDescending(x => x).ToArray();
            var arrayToCheck = new HeapSort().SortDesc(enumerable);
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }
    }
}
