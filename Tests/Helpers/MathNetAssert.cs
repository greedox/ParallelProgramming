using MathNet.Numerics.LinearAlgebra;
using Xunit;

namespace Tests.Helpers
{
    public class MathNetAssert
    {
        public static void Equal(Matrix<double> expected, Matrix<double> actual, int precision = 15)
        {
            Assert.True(expected.RowCount == actual.RowCount);
            Assert.True(expected.ColumnCount == actual.ColumnCount);
            for (int row = 0; row < expected.RowCount; row++)
            {
                for (int column = 0; column < expected.ColumnCount; column++)
                {
                    double expectedValue = expected[row, column];
                    double actualValue = actual[row, column];

                    Assert.Equal(expectedValue, actualValue, precision);
                }
            }
        }
    }
}
