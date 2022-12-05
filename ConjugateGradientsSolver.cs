namespace SystemOfLinearEq
{
    using System;
    using System.Linq;
    
    public static class ConjugateGradientsSolver
    {
        private const double Eps = 1e-5;

        /// <summary>
        /// Solve SLAE
        /// </summary>
        /// <param name="a">Matrix of coefficients</param>
        /// <param name="b">Vector of right side</param>
        /// <param name="x">Start values</param>
        public static (double[], int) Run(double[][] a, double[] b, double[] x)
        {
            var m = 0;
            var norm = 1d;

            var rPrev = a.MultiplyBy(x).SubtractBy(b);
            var p = (double[])rPrev.Clone();

            var lambda = ScalarMultiplication(p, rPrev) / ScalarMultiplication(p, a.MultiplyBy(p));

            x = x.SubtractBy(p.MultiplyBy(lambda));

            while (norm > Eps)
            {
                var r = rPrev.SubtractBy(a.MultiplyBy(p).MultiplyBy(lambda));
                var r2 = ScalarMultiplication(r, r);
                var alpha = r2 / ScalarMultiplication(rPrev, rPrev);

                p = r.SubtractBy(p.MultiplyBy(-alpha));
                lambda = r2 / ScalarMultiplication(p, a.MultiplyBy(p));
                x = x.SubtractBy(p.MultiplyBy(lambda));

                norm = Math.Sqrt(r2);
                m++;
                rPrev = (double[])r.Clone();
            }

            var result = x.Select(i => MathHelper.Round(i, Eps)).ToArray();
            return (result, m);
        }
        
        private static double[] MultiplyBy(this double[] v, double multiplier)
        {
            var n = v.Length;
            var result = new double[n];

            for (var i = 0; i < n; i++)
            {
                result[i] = v[i] * multiplier;
            }

            return result;
        }
        
        private static double[] MultiplyBy(this double[][] inputMatrix, double[] vector)
        {
            var n = vector.Length;
            var resultMatrix = new double[n];

            for (var i = 0; i < n; i++)
            {
                resultMatrix[i] = 0;

                for (var k = 0; k < n; k++)
                {
                    resultMatrix[i] += inputMatrix[i][k] * vector[k];
                }
            }

            return resultMatrix;
        }

        private static double[] SubtractBy(this double[] minuend, double[] subtrahend)
        {
            var n = minuend.Length;
            var result = new double[n];

            for (var i = 0; i < n; i++)
            {
                result[i] = minuend[i] - subtrahend[i];
            }

            return result;
        }

        private static double ScalarMultiplication(double[] v1, double[] v2)
        {
            var n = v1.Length;
            var result = 0d;

            for (var i = 0; i < n; i++)
            {
                result += v1[i] * v2[i];
            }

            return result;
        }
    }
}