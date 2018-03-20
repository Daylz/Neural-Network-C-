using System;

namespace Daylz.Mathf
{
    public static class MathExtension
    {
        public static float[] Sigmoid(float[] arr)
        {
            float[] result = new float[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = Sigmoid(arr[i]);
            }

            return result;
        }

        public static float Sigmoid(float x)
        {
            return 1 / (1 + (float)Math.Exp(-x));
        }
    }
}
