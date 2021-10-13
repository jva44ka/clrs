using System.Collections.Generic;
using CLRS.Core.Algorithms.Sorting;
using CLRS.Tests.Generators;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Sorting
{
    [TestFixture(Category = "Sorting")]
    public class QuickSortTests
    {
        private static List<List<int>> _testData = SortingTestDataGenerator.GetTestCases();

        [TestCaseSource(nameof(_testData))]
        public void QuickSortInts(List<int> enumerable)
        {
            SharedSortTests.TestAsc(enumerable, new QuickSort().Sort);
        }

        [TestCaseSource(nameof(_testData))]
        public void QuickSortInts_Desc(List<int> enumerable)
        {
            SharedSortTests.TestDesc(enumerable, new QuickSort().SortDesc);
        }
    }
}
