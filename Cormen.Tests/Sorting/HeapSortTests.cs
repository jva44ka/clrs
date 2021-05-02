using Cormen.Core.Algorithms.Sorting;
using Cormen.Tests.Data;
using Cormen.Tests.Generic;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cormen.Tests.Sorting
{
    [TestFixture(Category = "Sorting")]
    public class HeapSortTests
    {
        private static List<List<int>> _testData = SortingData.TestData;

        [TestCaseSource(nameof(_testData))]
        public void HeapSortInts(List<int> enumerable)
        {
            GenericSortTests.TestAsc(enumerable, new HeapSort().Sort);
        }

        [TestCaseSource(nameof(_testData))]
        public void HeapSortInts_Desc(List<int> enumerable)
        {
            GenericSortTests.TestDesc(enumerable, new HeapSort().SortDesc);
        }
    }
}
