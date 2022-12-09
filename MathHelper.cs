namespace SystemOfLinearEq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    public static class MathHelper
    {
        public static double Round(double x, double eps)
        {
            var i = 0;
            var newEps = eps;
            while (newEps < 1)
            {
                i++;
                newEps *= 10;
            }

            return Math.Round(x, i);
        }

        public static double GetNorm(IEnumerable<double> vector)
        {
            return Math.Sqrt(vector.Sum(i => Math.Pow(i, 2)));
        }

        public static double[] GetResidualFromTridiagonal(double[] x, double[] a, double[] b, double[] c, double[] f,  int n)
        {
            var residual = new double[n];
            
            residual[0] = c[0] * x[0] + b[0] * x[1] - f[0];
            for (var i = 1; i < n-1; i++)
            {
                residual[i] = a[i] * x[i - 1] + c[i] * x[i] + b[i] * x[i + 1] - f[i];
            }
            residual[n-1] = a[n - 1] * x[n - 2] + c[n - 1] * x[n - 1] - f[n - 1];

            return residual;
        }
    }
}