using Benchmark.Attributes;
using MathNet.Numerics.LinearAlgebra;
using Frobenius;

namespace Benchmark.Benchmarks
{
    public class FrobeniusBenchmark
    {
        private const int MatrixSize = 1000;
        private Matrix<double> _matrix;

        public FrobeniusBenchmark()
        {
            _matrix = Matrix<double>.Build.RandomSquare(MatrixSize);
        }

        [Benchmark]
        public void FrobeniosOneThread()
        {
            Algorithm.OneThreadFrobenius(_matrix);
        }

        [Benchmark]
        public void FrobeniosMultiThread()
        {
            Algorithm.MultiThreadFrobenius(_matrix);
        }
    }
}
