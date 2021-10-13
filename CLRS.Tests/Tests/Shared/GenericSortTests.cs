using System;
using System.Collections.Generic;
using System.Linq;
using Cormen.Core.Algorithms.Sorting.Interfaces;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Shared
{
    public class GenericSortTests
    {
        public static void TestAll(List<int> enumerable, ISortable sortClass)
        {
            TestAsc(enumerable, sortClass);
            TestDesc(enumerable, sortClass);
        }

        public static void TestAsc(List<int> enumerable, Func<IList<int>, IList<int>> sortFunc)
        {
            var sortedArray = enumerable.OrderBy(x => x).ToArray();
            var arrayToCheck = sortFunc(enumerable).ToArray();
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
        }

        public static void TestDesc(List<int> enumerable, Func<IList<int>, IList<int>> sortFunc)
        {
            var sortedArray = enumerable.OrderByDescending(x => x).ToArray();
            var arrayToCheck = sortFunc(enumerable).ToArray();
            CollectionAssert.AreEqual(sortedArray, arrayToCheck);
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
