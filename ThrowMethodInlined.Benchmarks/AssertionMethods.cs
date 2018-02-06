using System.Runtime.CompilerServices;

namespace ThrowMethodInlined.Benchmarks
{
    public static class AssertionMethods
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustHaveValue<T>(this T? parameter, string parameterName = null, string message = null) where T : struct
        {
            if (parameter.HasValue == false)
                Throw.NullableHasNoValue(parameterName, message);
            return parameter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MustNotHaveValue<T>(this T? parameter, string parameterName = null, string message = null) where T : struct
        {
            if (parameter.HasValue)
                Throw.NullableHasValue(parameter.Value, parameterName, message);
            return null;
        }
    }
}