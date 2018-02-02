using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;

namespace ThrowMethodInlined.Benchmarks
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }

    [ClrJob, CoreJob]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class Benchmarks
    {
        public static readonly int? NullableWithValue = 42;
        public static readonly int? NullableWithoutValue = null;

        [Benchmark (Baseline = true)]
        public static int? MustHaveValueBaseVersion()
        {
            if (NullableWithValue.HasValue == false) throw new NullableHasNoValueException(nameof(NullableWithValue));
            return NullableWithValue;
        }

        [Benchmark]
        public static int? MustHaveValueExtensionMethod() => NullableWithValue.MustHaveValue(nameof(NullableWithValue));

        [Benchmark]
        public static int? MustNotHaveValueBaseVersion()
        {
            if (NullableWithoutValue.HasValue) throw new NullableHasValueException(nameof(NullableWithoutValue));
            return NullableWithoutValue;
        }

        [Benchmark]
        public static int? MustNotHaveValueExtensionMethod() => NullableWithoutValue.MustNotHaveValue(nameof(NullableWithoutValue));
    }

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

    [Serializable]
    public class NullableHasNoValueException : ArgumentNullException
    {
        public NullableHasNoValueException(string parameterName = null, string message = null)
            : base(parameterName, message)
        {

        }
    }

    [Serializable]
    public class NullableHasValueException : ArgumentException
    {
        public NullableHasValueException(string parameterName = null, string message = null)
            : base(message, parameterName) { }
    }
}
