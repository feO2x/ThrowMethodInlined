using System;

namespace ThrowMethodInlined.Benchmarks
{
    [Serializable]
    public class NullableHasValueException : ArgumentException
    {
        public NullableHasValueException(string parameterName = null, string message = null)
            : base(message, parameterName) { }
    }
}