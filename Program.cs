namespace SystemOfLinearEq
{
    using System;

    internal static class Program
    {
        public static void Main(string[] args)
        {
            var a = new []
            {
                new double[] { 16, 2, 0, -2 },
                new double[] { 4, 20, 1, 0 },
                new double[] { 2, 0, 10, 0 },
                new double[] { -4, 0, 4, 32 },
            };
            var b = new double[] { -13, 24, 7, 0 };
            var x0 = new double[] { 0, 0, 0, 0 };
            
            // var (x, m) = SeidelSolver.Run(a, b, x0);
            var (x, m) = ConjugateGradientsSolver.Run(a, b, x0);
            
            Console.WriteLine("Решение системы: ");
            for (var i = 0; i < x.Length; i++)
                Console.WriteLine($"x{i} = {x[i]}");
            Console.WriteLine($"Итераций: {m}");
        }
    }
}