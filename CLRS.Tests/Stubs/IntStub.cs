using System;
using System.Collections.Generic;
using System.Text;

namespace CLRS.Tests.Stubs
{
    public class IntStub
    {
        private int _value;

        public IntStub(int value)
        {
            _value = value;
        }

        public static explicit operator IntStub(int intValue) => new IntStub(intValue);

        public override int GetHashCode()
        {
            return _value;
        }
    }
}
