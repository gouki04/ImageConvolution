using System;

namespace ImageConvolution
{
    public static class MathExtension
    {
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
                return min;
            else if (val.CompareTo(max) > 0)
                return max;
            else
                return val;
        }

        public static int Fmod(int a, int b)
        {
            return a - b * (int)Math.Floor(a / (float)b);
        }
    }
}
