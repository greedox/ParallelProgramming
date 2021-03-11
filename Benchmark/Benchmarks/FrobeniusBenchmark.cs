using Benchmark.Attributes;
using MathNet.Numerics.LinearAlgebra;
using Frobenius;

namespace Benchmark.Benchmarks
{
    public class FrobeniusBenchmark
    {
        private const int MatrixSize = 1000;

        public FrobeniusBenchmark()
        {

        }

        [Benchmark]
        public void FrobeniosOneThread()
        {
            var I = Matrix<double>.Build.RandomSquare(MatrixSize);
            Algorithm.OneThreadFrobenius(I, MatrixSize);
        }

        [Benchmark]
        public void FrobeniosMultiThread()
        {
            var I = Matrix<double>.Build.RandomSquare(MatrixSize);
            Algorithm.MultiThreadFrobenius(I, MatrixSize);
        }
    }
}
