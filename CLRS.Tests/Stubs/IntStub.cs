namespace CLRS.Tests.Stubs
{
    /// <summary>
    ///     Стаб Int32 с переопределеным GetHashCode, возвращающим этот же инт
    /// </summary>
    public class IntStub
    {
        protected int _value;

        public IntStub(int value)
        {
            _value = value;
        }

        public static explicit operator IntStub(int intValue) => new IntStub(intValue);

        public override int GetHashCode()
        {
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
    
    /// <summary>
    ///     Стаб интов с GetHashCode возвращающим всегда 0
    /// </summary>
    /// <returns>Для теста коллизий в хеш-таблице</returns>
    public class SameHashCodeIntStub : IntStub
    {
        public SameHashCodeIntStub(int value) : base(value)
        { }

        public static explicit operator SameHashCodeIntStub(int intValue)
        {
            return new SameHashCodeIntStub(intValue);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object? obj)
        {
            return _value.ToString().Equals(obj?.ToString());
        }
    }
}
