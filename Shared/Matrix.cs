using System;

namespace Shared
{
    public class Matrix
    {
        private readonly Random random = new Random();

        public Matrix(int m, int n, bool fillRandom = false)
        {
            Value = new int[m, n];

            if (fillRandom)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Value[i, j] = random.Next();
                    }
                }
            }
        }

        public int[,] Value { get; private set; }

        public void SetValue(int m, int n, int value)
        {
            Value[m, n] = value;
        }

        public int GetValue(int m, int n)
        {
            return Value[m, n];
        }
    }
}
