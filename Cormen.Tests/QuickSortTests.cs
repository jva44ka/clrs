using Cormen.Core.Algorithms.Sorting;
using Cormen.Tests.Data;
using Cormen.Tests.Generic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Tests
{
    public class QuickSortTests
    {
        private static List<List<int>> _testData = SortingData.TestData;

        [TestCaseSource(nameof(_testData))]
        public void QuickSortInts(List<int> enumerable)
        {
            GenericSortTests.Test(enumerable, new QuickSort());
        }

        [TestCaseSource(nameof(_testData))]
        public void QuickSortInts_Desc(List<int> enumerable)
        {
            GenericSortTests.Test(enumerable, new QuickSort());
        }
    }
}
