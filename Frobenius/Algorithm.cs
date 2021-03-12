using System;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Frobenius
{
    /// <summary>
    /// https://vunivere.ru/work14851/page4
    /// </summary>
    public class Algorithm
    {
        public static Matrix<double> OneThreadFrobenius(Matrix<double> matrix)
        {
            int size = matrix.RowCount;
            if (matrix.ColumnCount != size)
            {
                throw new ArgumentException($"{nameof(matrix)} should be a square matrix");
            }

            int halfSize = size / 2;
            var A = matrix.SubMatrix(0, halfSize, 0, halfSize);
            var B = matrix.SubMatrix(0, halfSize, halfSize, halfSize);
            var C = matrix.SubMatrix(halfSize, halfSize, 0, halfSize);
            var D = matrix.SubMatrix(halfSize, halfSize, halfSize, halfSize);
            var H = D - C * A.Inverse() * B;

            var M1 = A.Inverse() + A.Inverse() * B * H.Inverse() * C * A.Inverse();
            var M2 = (A.Inverse() * B * H.Inverse()).Multiply(-1);
            var M3 = (H.Inverse() * C * A.Inverse()).Multiply(-1);
            var M4 = H.Inverse();

            var M = Matrix<double>.Build.Dense(size, size);
            M.SetSubMatrix(0, 0, M1);
            M.SetSubMatrix(0, halfSize, M2);
            M.SetSubMatrix(halfSize, 0, M3);
            M.SetSubMatrix(halfSize, halfSize, M4);

            return M;
        }

        public static Matrix<double> MultiThreadFrobenius(Matrix<double> matrix)
        {
            int size = matrix.RowCount;
            if (matrix.ColumnCount != size)
            {
                throw new ArgumentException($"{nameof(matrix)} should be a square matrix");
            }

            int halfSize = size / 2;
            var A = matrix.SubMatrix(0, halfSize, 0, halfSize);
            var B = matrix.SubMatrix(0, halfSize, halfSize, halfSize);
            var C = matrix.SubMatrix(halfSize, halfSize, 0, halfSize);
            var D = matrix.SubMatrix(halfSize, halfSize, halfSize, halfSize);
            var H = D - C * A.Inverse() * B;

            Matrix<double> M1 = null;
            Matrix<double> M2 = null;
            Matrix<double> M3 = null;
            Matrix<double> M4 = null;

            var frobeniusActions = new Action[]
            {
                () => M1 = A.Inverse() + A.Inverse() * B * H.Inverse() * C * A.Inverse(),
                () => M2 = (A.Inverse() * B * H.Inverse()).Multiply(-1),
                () => M3 = (H.Inverse() * C * A.Inverse()).Multiply(-1),
                () => M4 = H.Inverse()
            };
            Parallel.Invoke(frobeniusActions);

            var M = Matrix<double>.Build.Dense(size, size);
            M.SetSubMatrix(0, 0, M1);
            M.SetSubMatrix(0, halfSize, M2);
            M.SetSubMatrix(halfSize, 0, M3);
            M.SetSubMatrix(halfSize, halfSize, M4);

            return M;
        }
    }
}
