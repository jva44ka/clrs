using Cormen.Core.Algorithms.Sorting;
using Cormen.Tests.Data;
using Cormen.Tests.Generic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cormen.Tests
{
    [TestFixture(Category = "Sorting")]
    public class CountingSortTests
    {
        private static List<List<int>> _testData = SortingData.TestData;

        [TestCaseSource(nameof(_testData))]
        public void CountingSort_Asc(List<int> enumerable)
        {
            GenericSortTests.TestAsc(enumerable, new CountingSort().Sort);
        }

        [TestCaseSource(nameof(_testData))]
        public void CountingSort_Desc(List<int> enumerable)
        {
            GenericSortTests.TestDesc(enumerable, new CountingSort().SortDesc);
        }
    }
}
