using System;

namespace Benchmark
{
    public class BenchmarkResult
    {
        public BenchmarkResult(TimeSpan executionTime)
        {
            ExecutionTime = executionTime;
        }

        public TimeSpan ExecutionTime { get; private set; }
    }
}
