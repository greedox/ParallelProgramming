using System;

namespace Benchmark.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgnoreBenchmarkAttribute : Attribute
    {
    }
}
