namespace SystemOfLinearEq
{
    using System;
    
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
    }
}