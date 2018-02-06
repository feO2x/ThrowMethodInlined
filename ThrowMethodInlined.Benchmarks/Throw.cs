using System.Runtime.CompilerServices;

namespace ThrowMethodInlined.Benchmarks
{
    public static class Throw
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void NullableHasNoValue(string parameterName = null, string message = null)
        {
            throw new NullableHasNoValueException(parameterName, message ?? $"{parameterName ?? "The nullable"} must have a value, but it actually is null.");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void NullableHasValue<T>(T value, string parameterName = null, string message = null) where T : struct
        {
            throw new NullableHasValueException(parameterName, message ?? $"{parameterName ?? "The nullable"} must have no value, but it actually is \"{value}\".");
        }
    }
}