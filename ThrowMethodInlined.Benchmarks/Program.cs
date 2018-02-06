using BenchmarkDotNet.Running;

namespace ThrowMethodInlined.Benchmarks
{
    internal static class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<IntBenchmarks>();
        }
    }
}
