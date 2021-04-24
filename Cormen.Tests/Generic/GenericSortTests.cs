using Cormen.Core.Algorithms.Sorting.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Cormen.Tests.Generic
{
    public class GenericSortTests
    {
        public static void Test(List<int> enumerable, ISortable sortClass)
        {
            TestAsc(enumerable, sortClass);
            TestDesc(enumerable, sortClass);
        }

        private static void TestAsc(IList<int> enumerable, ISortable sortClass)
        {
            var sortedArray = enumerable.OrderBy(x => x).ToArray();
            var arrayToCheck = sortClass.Sort(enumerable).ToArray();
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }

        private static void TestDesc(IList<int> enumerable, ISortable sortClass)
        {
            var sortedArray = enumerable.OrderByDescending(x => x).ToArray();
            var arrayToCheck = sortClass.SortDesc(enumerable).ToArray();
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }
    }
}
