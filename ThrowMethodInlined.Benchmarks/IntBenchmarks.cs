using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;

namespace ThrowMethodInlined.Benchmarks
{
    [ClrJob, CoreJob]
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class IntBenchmarks
    {
        public readonly int? NullableWithValue = 42;
        public readonly int? NullableWithoutValue = null;

        [Benchmark(Baseline = true)]
        public int? MustHaveValueBaseVersion()
        {
            if (NullableWithValue.HasValue == false) throw new NullableHasNoValueException(nameof(NullableWithValue));
            return NullableWithValue;
        }

        [Benchmark]
        public int? MustHaveValueExtensionMethod()
        {
            return NullableWithValue.MustHaveValue(nameof(NullableWithValue));
        }
        
        

        [Benchmark]
        public int? MustNotHaveValueBaseVersion()
        {
            if (NullableWithoutValue.HasValue) throw new NullableHasValueException(nameof(NullableWithoutValue));
            return NullableWithoutValue;
        }

        [Benchmark]
        public int? MustNotHaveValueExtensionMethod() => NullableWithoutValue.MustNotHaveValue(nameof(NullableWithoutValue));
    }
}