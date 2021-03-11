using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Benchmark.Attributes;
using Shared;

namespace Benchmark
{
    public class BenchmarkRunner
    {
        public static void Run<T>(int count = 1) where T : class
        {
            if (count < 1)
            {
                throw new ArgumentException($"{nameof(count)} should be greater or equal than '1'");
            }

            var benhmarkInstance = (T)Activator.CreateInstance(typeof(T));

            var methods = typeof(T).GetMethods()
                      .Where(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Length > 0)
                      .ToArray();

            var bencmarkResults = new Dictionary<string, List<BenchmarkResult>>();

            for (int i = 0; i < count; i++)
            {
                foreach (var method in methods)
                {
                    var benchmarkResult = InvokeBenchmark(() => method.Invoke(benhmarkInstance, null));

                    bencmarkResults.TryGetValue(method.Name, out var value);
                    value ??= new List<BenchmarkResult>();

                    value.Add(benchmarkResult);
                    bencmarkResults[method.Name] = value;
                }
            }

            var table = bencmarkResults.ToStringTable(
                new[] { "Method", "Mean Time", "Std. Dev." },
                r => r.Key, r => r.Value.Average(x => x.ExecutionTime), r => r.Value.StdDev(s => s.ExecutionTime));
            Console.WriteLine(table);
        }

        private static BenchmarkResult InvokeBenchmark(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            action();

            stopwatch.Stop();
            return new BenchmarkResult(stopwatch.Elapsed);
        }
    }
}
