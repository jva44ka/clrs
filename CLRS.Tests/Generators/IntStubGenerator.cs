using System.Collections.Generic;
using CLRS.Tests.Stubs;

namespace CLRS.Tests.Generators
{
    public static class IntStubGenerator
    {
        public static List<IntStub> GetFirst100Numbers()
        {
            var numbers = new List<IntStub>();
            for (int i = 0; i < 100; i++)
            {
                numbers.Add((IntStub)i);
            }

            return numbers;
        }
    }
}