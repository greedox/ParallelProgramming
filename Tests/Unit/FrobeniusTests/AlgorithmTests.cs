using Frobenius;
using MathNet.Numerics.LinearAlgebra;
using Tests.Helpers;
using Xunit;

namespace Tests.Unit.FrobeniusTests
{
    public class AlgorithmTests
    {
        private const int Precision = 5;
        private Matrix<double> _matrix;

        public AlgorithmTests()
        {
            _matrix = Matrix<double>.Build.RandomSquare(100);
        }

        [Fact]
        public void ShouldReturnInverseMatrix_IfUseOneThreadAlgorithm()
        {
            var expected = _matrix.Inverse();
            var actual = Algorithm.OneThreadFrobenius(_matrix);

            MathNetAssert.Equal(expected, actual, Precision);
        }

        [Fact]
        public void ShouldReturnInverseMatrix_IfUseMultiThreadAlgorithm()
        {
            var expected = _matrix.Inverse();
            var actual = Algorithm.MultiThreadFrobenius(_matrix);

            MathNetAssert.Equal(expected, actual, Precision);
        }

        [Fact]
        public void ShouldReturnEqualMatrix_IfUseOneAndMultiThreadAlgorithms()
        {
            var oneThreadResult = Algorithm.OneThreadFrobenius(_matrix);
            var multiThreadResult = Algorithm.MultiThreadFrobenius(_matrix);

            MathNetAssert.Equal(oneThreadResult, multiThreadResult);
        }
    }
}
