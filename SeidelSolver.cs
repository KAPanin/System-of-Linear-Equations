namespace SystemOfLinearEq
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	
    public static class SeidelSolver
    {
	    private const double Eps = 1e-5;

	    public static (double[], double[], double[], double[]) TridiagonalRun(double[] a, double[] b, double[] c, double[] f,  int n)
	    {
		    var m = 0;
		    var mArray = new List<double>();
		    var normArray = new List<double>();
		    double[] finalResidual;

		    var result = new double[n];
		    result[0] = 0;
		    result[n - 1] = 1;

		    while (true)
		    {
			    for (var i = 1; i < n - 1; i++)
			    {
				    result[i] = (f[i] - a[i] * result[i - 1] - b[i] * result[i + 1]) / c[i];
			    }

			    m++;
			    var residual = MathHelper.GetResidualFromTridiagonal(result, a, b, c, f, n);
			    var norm = MathHelper.GetNorm(residual);
			    mArray.Add(m);
			    normArray.Add(norm);

			    if (norm > Eps)
				    continue;
			    
			    finalResidual = residual;
			    break;
		    }

		    return (result, finalResidual, mArray.ToArray(), normArray.ToArray());
	    }

	    /// <summary>
	    /// Solve SLAE 
	    /// </summary>
	    /// <param name="a">Matrix of coefficients</param>
	    /// <param name="b">Vector of right side</param>
	    /// <param name="x0">Start values</param>
	    public static (double[], int) Run(double[][] a, double[] b, double[] x0)
	    {
		    var n = x0.Length;
		    var m = 0;
			
		    var x = (double[]) x0.Clone();

		    Console.WriteLine("Диагональное преобладание: ");
		    if (!IsDiagonal(a, n))
		    {
			    Console.WriteLine("Не выполняется преобладание диагоналей");
			    return (new double[n], m);
		    }

		    do
		    {
			    x0 = (double[]) x.Clone();
			    for (var i = 0; i < n; i++)
			    {
				    double var = 0;
				    for (var j = 0; j < n; j++)
					    if (j != i) 
						    var += a[i][j] * x[j];
					
				    x[i] = (b[i] - var) / a[i][i];
			    }
			    m++;
		    } while (!Converge(x, x0, n));

		    var result = x.Select(i => MathHelper.Round(i, Eps)).ToArray();
		    return (result, m);
	    }

	    private static bool Converge(double[] xk, double[] xkp, int n)
		{
			double norm = 0;
			for (var i = 0; i < n; i++)
				norm += (xk[i] - xkp[i]) * (xk[i] - xkp[i]);
			return Math.Sqrt(norm) < Eps;
		}

	    private static bool IsDiagonal(double[][] a, int n)
		{
			var k = 1;
			double sum;
			for (var i = 0; i < n; i++) {
				sum = 0;
				for (var j = 0; j < n; j++) sum += Math.Abs(a[i][j]);
				sum -= Math.Abs(a[i][i]);
				if (sum > a[i][i]) 
				{
					k = 0;
					Console.WriteLine($"{a[i][i]} < {sum}");
				}
				else
				{
					Console.WriteLine($"{a[i][i]} > {sum}");
				}
			}

			return k == 1;
		}
    }
}