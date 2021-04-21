using Cormen.Core.Algorithms.Sorting;
using Cormen.Tests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cormen.Tests
{
    [TestFixture(Category = "Sorting")]
    public class MergeSortTests
    {
        private static List<List<int>> _testData = SortingHelper.TestData;

        [TestCaseSource(nameof(_testData))]
        public void MergeSortInts(List<int> enumerable)
        {
            var sortedArray = enumerable.OrderBy(x => x).ToArray();
            var arrayToCheck = new MergeSort().Sort(enumerable);
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }

        [TestCaseSource(nameof(_testData))]
        public void MergeSortInts_Desc(List<int> enumerable)
        {
            var sortedArray = enumerable.OrderByDescending(x => x).ToArray();
            var arrayToCheck = new MergeSort().SortDesc(enumerable);
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }
    }
}
