using System;

namespace Benchmark.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BenchmarkAttribute : Attribute
    {
    }
}
