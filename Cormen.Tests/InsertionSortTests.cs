using Cormen.Core.Algorithms.Sorting;
using Cormen.Tests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cormen.Tests
{
    [TestFixture(Category = "Sorting")]
    public class InsertionSortTests
    {
        private static List<List<int>> _testData = SortingHelper.TestData;

        [TestCaseSource(nameof(_testData))]
        public void InsertionSortInts(List<int> enumerable)
        {
            var sortedArray = enumerable.OrderBy(x => x).ToArray();
            var arrayToCheck = new InsertionSortClass().Sort(enumerable);
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }

        [TestCaseSource(nameof(_testData))]
        public void InsertionSortInts_Desc(List<int> enumerable)
        {
            var sortedArray = enumerable.OrderByDescending(x => x).ToArray();
            var arrayToCheck = new InsertionSortClass().SortDesc(enumerable);
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }
    }
}
