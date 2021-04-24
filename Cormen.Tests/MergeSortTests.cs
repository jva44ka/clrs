﻿using Cormen.Core.Algorithms.Sorting;
using Cormen.Tests.Data;
using Cormen.Tests.Generic;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cormen.Tests
{
    [TestFixture(Category = "Sorting")]
    public class MergeSortTests
    {
        private static List<List<int>> _testData = SortingData.TestData;

        [TestCaseSource(nameof(_testData))]
        public void MergeSortInts(List<int> enumerable)
        {
            GenericSortTests.Test(enumerable, new MergeSort());
        }

        [TestCaseSource(nameof(_testData))]
        public void MergeSortInts_Desc(List<int> enumerable)
        {
            GenericSortTests.Test(enumerable, new MergeSort());
        }
    }
}
