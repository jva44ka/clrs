using System.Collections.Generic;
using CLRS.Core.Algorithms.Sorting;
using CLRS.Tests.Generators;
using CLRS.Tests.Tests.Shared;
using NUnit.Framework;

namespace CLRS.Tests.Tests.Sorting
{
    [TestFixture(Category = "Sorting")]
    public class InsertionSortTests
    {
        private static List<List<int>> _testData = SortingTestDataGenerator.GetTestCases();

        [TestCaseSource(nameof(_testData))]
        public void InsertionSortInts(List<int> enumerable)
        {
            GenericSortTests.TestAsc(enumerable, new InsertionSort().Sort);
        }

        [TestCaseSource(nameof(_testData))]
        public void InsertionSortInts_Desc(List<int> enumerable)
        {
            GenericSortTests.TestDesc(enumerable, new InsertionSort().SortDesc);
        }
    }
}
