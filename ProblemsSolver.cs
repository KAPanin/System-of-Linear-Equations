namespace SystemOfLinearEq
{
    using System;
    using System.Diagnostics;
    
    public static class ProblemsSolver
    {
        public static void SolveTestProblem()
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

        public static void SolveOneDimensionPoissonEquation()
        {
            double F(double x)
            {
                return Math.Round(16 * Math.Pow(Math.PI, 2) * Math.Sin(16 * Math.PI * x), 9);
            }

            double ExactSolution(double x)
            {
                return Math.Round(x - Math.Sin(16 * Math.PI * x) / 16, 9);
            }

            const int n = 500;
            const double h = 1d / (n-1);
            const double k = 1d;
            
            var x = new double[n];
            var f = new double[n];
            var exactSolution = new double[n];
            for (var i = 0; i < n; i++)
            {
                x[i] = i * h;
                f[i] = F(x[i]);
                exactSolution[i] = ExactSolution(x[i]);
            }

            var a = new double[n];
            var b = new double[n];
            var c = new double[n];
            
            c[0] = 1;
            for (var i = 1; i < n - 1; i++)
            {
                a[i] = k / Math.Pow(h, 2);
                b[i] = k / Math.Pow(h, 2);
                c[i] = - 2 *k / Math.Pow(h, 2);
            }

            var watch = Stopwatch.StartNew();
            var thomasResult = ThomasSolver.Run(a, b, c, f, n);
            watch.Stop(); 
            Console.WriteLine($"Thomas time {watch.Elapsed.TotalMilliseconds} ms.");
            
            watch = Stopwatch.StartNew();
            var (seidelResult, seidelResidual, seidelIters, seidelNorms) =
                SeidelSolver.TridiagonalRun(a, b, c, f, n);
            watch.Stop(); 
            Console.WriteLine($"Seidel time {watch.Elapsed.TotalMilliseconds} ms.");
            
            watch = Stopwatch.StartNew();
            var (conjugateGradientsResult, conjugateGradientsResidual, conjugateGradientsIters, conjugateGradientsNorms) = 
                ConjugateGradientsSolver.TridiagonalRun(a, b, c, f, n);
            watch.Stop(); 
            Console.WriteLine($"Conjugate Gradients time {watch.Elapsed.TotalMilliseconds} ms.");
            
            FileHelper.WriteFile("data.txt", x, thomasResult, seidelResult, conjugateGradientsResult, exactSolution,
                seidelResidual, conjugateGradientsResidual);
            FileHelper.WriteFile("seidelResiduals.txt", seidelIters, seidelNorms);
            FileHelper.WriteFile("conjugateGradientsResiduals.txt", conjugateGradientsIters, conjugateGradientsNorms);
        }
    }
}