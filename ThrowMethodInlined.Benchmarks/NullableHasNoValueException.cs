using System;

namespace ThrowMethodInlined.Benchmarks
{
    [Serializable]
    public class NullableHasNoValueException : ArgumentNullException
    {
        public NullableHasNoValueException(string parameterName = null, string message = null)
            : base(parameterName, message) { }
    }
}