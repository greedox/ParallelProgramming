using System;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace Frobenius
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input n: ");
            int n = int.Parse(Console.ReadLine());
            var I = Matrix<double>.Build.Random(n, n);

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("Iteration 1.");
                InvokeWithTimer("One thread", () => OneThreadFrobenius(I, n));
                InvokeWithTimer("Multithread V1", () => MultiThreadFrobenius(I, n));
                Console.WriteLine("==========");
            }

            Console.ReadKey();
        }

        private static void OneThreadFrobenius(Matrix<double> I, int n)
        {
            var A = I.SubMatrix(0, n / 2, 0, n / 2);
            var B = I.SubMatrix(0, n / 2, n / 2, n / 2);
            var C = I.SubMatrix(n / 2, n / 2, 0, n / 2);
            var D = I.SubMatrix(n / 2, n / 2, n / 2, n / 2);
            var H = D - C * A.Inverse() * B;

            var M1 = A.Inverse() + A.Inverse() * B * H.Inverse() * C * A.Inverse();
            var M2 = (A.Inverse() * B * H.Inverse()).Multiply(-1);
            var M3 = (H.Inverse() * C * A.Inverse()).Multiply(-1);
            var M4 = H.Inverse();

            var M = Matrix<double>.Build.Dense(n, n);
            M.SetSubMatrix(0, 0, M1);
            M.SetSubMatrix(0, n / 2, M2);
            M.SetSubMatrix(n / 2, 0, M3);
            M.SetSubMatrix(n / 2, n / 2, M4);
        }

        private static void MultiThreadFrobenius(Matrix<double> I, int n)
        {
            var A = I.SubMatrix(0, n / 2, 0, n / 2);
            var B = I.SubMatrix(0, n / 2, n / 2, n / 2);
            var C = I.SubMatrix(n / 2, n / 2, 0, n / 2);
            var D = I.SubMatrix(n / 2, n / 2, n / 2, n / 2);
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

            var M = Matrix<double>.Build.Dense(n, n);
            M.SetSubMatrix(0, 0, M1);
            M.SetSubMatrix(0, n / 2, M2);
            M.SetSubMatrix(n / 2, 0, M3);
            M.SetSubMatrix(n / 2, n / 2, M4);
        }

        private static void InvokeWithTimer(string title, Action action)
        {
            var start = DateTime.Now;

            action();

            var end = DateTime.Now;

            Console.WriteLine($"{title}: {end - start}");
        }
    }
}
