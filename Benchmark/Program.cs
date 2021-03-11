using Benchmark.Benchmarks;

namespace Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FrobeniusBenchmark>(10);
        }
    }
}
