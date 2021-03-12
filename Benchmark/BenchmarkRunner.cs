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
        /// <summary>
        /// Run methods with <see cref="BenchmarkAttribute"/>
        /// </summary>
        /// <typeparam name="T">Benchmark class</typeparam>
        /// <param name="instanceCreation"><see cref="InstanceCreation"/></param>
        /// <param name="count">Count of method runs</param>
        public static void Run<T>(InstanceCreation instanceCreation = InstanceCreation.Transient, int count = 15, bool showIntermediateResult = true) where T : class
        {
            if (count < 1)
            {
                throw new ArgumentException($"{nameof(count)} should be greater or equal than '1'");
            }

            Type benchmarkType = typeof(T);

            var methods = benchmarkType.GetMethods()
                      .Where(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Length > 0)
                      .ToArray();

            var bencmarkResults = new Dictionary<string, List<BenchmarkResult>>();

            T benchmarkInstance = null;
            if (instanceCreation == InstanceCreation.Singleton)
            {
                benchmarkInstance = CreateInstance<T>(benchmarkType);
            }

            for (int i = 0; i < count; i++)
            {
                if (instanceCreation == InstanceCreation.Transient)
                {
                    benchmarkInstance = CreateInstance<T>(benchmarkType);
                }

                foreach (var method in methods)
                {
                    if (instanceCreation == InstanceCreation.Scoped)
                    {
                        benchmarkInstance = CreateInstance<T>(benchmarkType);
                    }

                    if (benchmarkInstance == null)
                    {
                        throw new ArgumentNullException(nameof(benchmarkInstance));
                    }

                    var benchmarkResult = InvokeBenchmark(() => method.Invoke(benchmarkInstance, null));
                    SaveBenchmarkResult(bencmarkResults, method, benchmarkResult);

                    if (showIntermediateResult)
                    {
                        Console.WriteLine($"{i + 1}. {method.Name}: {benchmarkResult.ExecutionTime}");
                    }
                }
            }

            ShowResultTable(benchmarkType, bencmarkResults);
        }

        private static void ShowResultTable(Type benchmarkType, Dictionary<string, List<BenchmarkResult>> bencmarkResults)
        {
            var className = benchmarkType.FullName;
            Console.WriteLine($"Class report ({className})");
            var table = bencmarkResults.ToStringTable(
                new[] { "Method", "Mean Time", "Std. Dev." },
                r => r.Key, r => r.Value.Average(x => x.ExecutionTime), r => r.Value.StdDev(s => s.ExecutionTime));
            Console.WriteLine(table);
        }

        private static void SaveBenchmarkResult(Dictionary<string, List<BenchmarkResult>> bencmarkResults, System.Reflection.MethodInfo method, BenchmarkResult benchmarkResult)
        {
            bencmarkResults.TryGetValue(method.Name, out var value);
            value ??= new List<BenchmarkResult>();

            value.Add(benchmarkResult);
            bencmarkResults[method.Name] = value;
        }

        private static T CreateInstance<T>(Type benchmarkType) where T : class
        {
            return (T)Activator.CreateInstance(benchmarkType);
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
