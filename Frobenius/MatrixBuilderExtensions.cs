using System;
using MathNet.Numerics.LinearAlgebra;

namespace Frobenius
{
    public static class MatrixBuilderExtensions
    {
        public static Matrix<T> RandomSquare<T>(this MatrixBuilder<T> matrixBuilder, int n) where T : struct, IEquatable<T>, IFormattable
        {
            return matrixBuilder.Random(n, n);
        }
    }
}
