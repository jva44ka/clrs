using System.Collections.Generic;
using CLRS.Core.Algorithms.Sorting;
using CLRS.Tests.Generators;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Sorting
{
    [TestFixture(Category = "Sorting")]
    public class MergeSortTests
    {
        private static List<List<int>> _testData = SortingTestDataGenerator.GetTestCases();

        [TestCaseSource(nameof(_testData))]
        public void MergeSortInts(List<int> enumerable)
        {
            SharedSortTests.TestAsc(enumerable, new MergeSort().Sort);
        }

        [TestCaseSource(nameof(_testData))]
        public void MergeSortInts_Desc(List<int> enumerable)
        {
            SharedSortTests.TestDesc(enumerable, new MergeSort().SortDesc);
        }
    }
}
