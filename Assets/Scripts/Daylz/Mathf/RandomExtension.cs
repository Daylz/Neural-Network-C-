using System;

namespace Daylz.Mathf
{
    public static class RandomExtension
    {
        public static float NextGaussianFloat(this Random r)
        {
            double u, v, S;

            do
            {
                u = 2.0 * r.NextDouble() - 1.0;
                v = 2.0 * r.NextDouble() - 1.0;
                S = u * u + v * v;
            }
            while (S >= 1.0);

            double fac = Math.Sqrt(-2.0 * Math.Log(S) / S);
            return (float)(u * fac);
        }
    }
}
