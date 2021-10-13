using System.Collections.Generic;

namespace CLRS.Tests.Generators
{
    /// <summary>
    ///     Генератор данных для тестов сортировки
    /// </summary>
    public class SortingTestDataGenerator
    {
        private readonly List<List<int>> _cases = new List<List<int>>();

        public static SortingTestDataGenerator New() => new SortingTestDataGenerator();

        /// <summary>
        ///     Неубывающий порядок, например "1, 2, 3"
        /// </summary>
        public SortingTestDataGenerator WithNonDecreasing()
        {
            _cases.Add(new List<int> { 1, 2, 3, 4, 5, 6 });
            return this;
        }

        /// <summary>
        ///     Неубывающий порядок с повторениями, например "1, 2, 2"
        /// </summary>
        public SortingTestDataGenerator WithNonDecreasingWithRepetitions()
        {
            _cases.Add(new List<int> { 1, 2, 2, 2, 5, 6 });
            _cases.Add(new List<int> { 2, 2, 3 });
            return this;
        }
        
        /// <summary>
        ///     Невозрастающий порядок, например "3, 2, 1"
        /// </summary>
        public SortingTestDataGenerator WithNonIncreasing()
        {
            _cases.Add(new List<int> { 6, 5, 4, 3, 2, 1 });
            return this;
        }

        /// <summary>
        ///     Невозрастающий порядок с повторениями, например "3, 2, 2"
        /// </summary>
        public SortingTestDataGenerator WithNonIncreasingWithRepetitions()
        {
            _cases.Add(new List<int> { 6, 5, 4, 4, 2, 1 });
            _cases.Add(new List<int> { 2, 3, 3 });
            return this;
        }
        
        /// <summary>
        ///     Набор одинаковых значений, например "1, 1, 1, 1"
        /// </summary>
        public SortingTestDataGenerator WithEqualValues()
        {
            _cases.Add(new List<int> { 1, 1, 1, 1, 1, 1 });
            _cases.Add(new List<int> { 1000, 1000, 1000, 1000, 1000, 1000 });
            return this;
        }
        
        /// <summary>
        ///     Набор неотсортированных значений, например "4, 2, 3, 1"
        /// </summary>
        public SortingTestDataGenerator WithNotOrderedValues()
        {
            _cases.Add(new List<int> { 60, 500, 40, 300, 20, 100 });
            return this;
        }

        public List<List<int>> Build() => _cases;

        /// <summary>
        ///     Наборы тестовых данных, покрывающие самые распространенные случаи
        ///     и позволяющие выявлять большинство багов сортировки
        /// </summary>
        public static List<List<int>> GetTestCases() => New().WithNonDecreasing()
                                                             .WithNonDecreasingWithRepetitions()
                                                             .WithNonIncreasing()
                                                             .WithNonIncreasingWithRepetitions()
                                                             .WithEqualValues()
                                                             .WithNotOrderedValues()
                                                             .Build();
    }
}
