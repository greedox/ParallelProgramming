using Benchmark.Attributes;
using MathNet.Numerics.LinearAlgebra;
using Frobenius;

namespace Benchmark.Benchmarks
{
    public class FrobeniusBenchmark
    {
        private const int SmallMatrixSize = 100;
        private const int MediumMatrixSize = 500;
        private const int LargeMatrixSize = 1000;
        private Matrix<double> _smallMatrix;
        private Matrix<double> _mediumMatrix;
        private Matrix<double> _largeMatrix;

        public FrobeniusBenchmark()
        {
            _smallMatrix = CreateRandomMatrix(SmallMatrixSize);
            _mediumMatrix = CreateRandomMatrix(MediumMatrixSize);
            _largeMatrix = CreateRandomMatrix(LargeMatrixSize);
        }

        [Benchmark]
        public void FrobeniosOneThreadSmall()
        {
            Algorithm.OneThreadFrobenius(_smallMatrix);
        }

        [Benchmark]
        public void FrobeniosMultiThreadSmall()
        {
            Algorithm.MultiThreadFrobenius(_smallMatrix);
        }
        
        [Benchmark]
        public void FrobeniosOneThreadMedium()
        {
            Algorithm.OneThreadFrobenius(_mediumMatrix);
        }

        [Benchmark]
        public void FrobeniosMultiThreadMedium()
        {
            Algorithm.MultiThreadFrobenius(_mediumMatrix);
        }

        [Benchmark]
        [IgnoreBenchmark]
        public void FrobeniosOneThreadLarge()
        {
            Algorithm.OneThreadFrobenius(_largeMatrix);
        }

        [Benchmark]
        [IgnoreBenchmark]
        public void FrobeniosMultiThreadLarge()
        {
            Algorithm.MultiThreadFrobenius(_largeMatrix);
        }

        private Matrix<double> CreateRandomMatrix(int size)
        {
            return Matrix<double>.Build.RandomSquare(size);
        }
    }
}
