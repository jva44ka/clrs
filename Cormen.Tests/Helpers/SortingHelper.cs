using System.Collections.Generic;

namespace Cormen.Tests.Helpers
{
    public class SortingHelper
    {
        internal static List<List<int>> TestData = new List<List<int>>
        {
            new List<int> { 1, 2, 3, 4, 5, 6 },
            new List<int> { 1, 2, 2, 2, 5, 6 },
            new List<int> { 6, 5, 4, 3, 2, 1 },
            new List<int> { 1, 1, 1, 1, 1, 1 },
            new List<int> { 60, 500, 40, 300, 20, 100 },
            new List<int> { 1000, 1000, 1000, 1000, 1000, 1000 },
            new List<int> { 2, 2, 3 },
            new List<int> { 2, 3, 3 },
        };
    }
}
