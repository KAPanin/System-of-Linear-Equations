namespace SystemOfLinearEq
{
    public static class ThomasSolver
    {
        public static double[] Run(double[] a, double[] b, double[] c, double[] f,  int n)
        {
            var result = new double[n];

            var alpha = new double[n];
            var beta = new double[n];

            alpha[0] = b[0] / c[0];
            beta[0] = -f[0] / c[0];
            for (var i = 0; i < n - 1; i++)
            {
                var k = - c[i] - alpha[i] * a[i];
                alpha[i + 1] = b[i] / k;
                beta[i + 1] = (-f[i] + beta[i] * a[i]) / k;
            }

            result[n - 1] = 1;
            for (var i = n - 2; i >= 0; i--)
            {
                result[i] = alpha[i + 1] * result[i + 1] + beta[i + 1];
            }

            return result;
        }
    }
}