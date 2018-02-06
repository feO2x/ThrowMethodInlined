using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;

namespace ThrowMethodInlined.Benchmarks
{
    [ClrJob, CoreJob]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class DoubleBenchmarks
    {
        public readonly double? NullableWithValue = 42.87;
        public readonly double? NullableWithoutValue = null;

        [Benchmark(Baseline = true)]
        public double? MustHaveValueBaseVersion()
        {
            if (NullableWithValue.HasValue == false) throw new NullableHasNoValueException(nameof(NullableWithValue));
            return NullableWithValue;
        }

        [Benchmark]
        public double? MustHaveValueExtensionMethod() => NullableWithValue.MustHaveValue(nameof(NullableWithValue));

        [Benchmark]
        public double? MustNotHaveValueBaseVersion()
        {
            if (NullableWithoutValue.HasValue) throw new NullableHasValueException(nameof(NullableWithoutValue));
            return NullableWithoutValue;
        }

        [Benchmark]
        public double? MustNotHaveValueExtensionMethod() => NullableWithoutValue.MustNotHaveValue(nameof(NullableWithoutValue));
    }
}