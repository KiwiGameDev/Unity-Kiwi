using System;
using System.Diagnostics.Contracts;

namespace Kiwi.Utilities
{
    public static class MoreMath
    {
        public static readonly float APPROXIMATION_TOLERANCE = 0.000001f;
        public static readonly float TAU = (float) Math.PI * 2f;
        public static readonly float PI_HALF = (float) Math.PI / 2f;

        [Pure]
        public static int RoundUpToNearestPowerOf2(int value)
        {
            if (value < 0)
                return 0;

            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;

            return value + 1;
        }

        [Pure]
        public static int RoundDownToNearestPowerOf2(int value)
        {
            return RoundUpToNearestPowerOf2(value) >> 1;
        }

        [Pure]
        public static double Map(double value, double x1, double y1, double x2, double y2)
        {
            return (value - x1) / (y1 - x1) * (y2 - x2) + x2;
        }

        [Pure]
        public static float Map(float value, float x1, float y1, float x2, float y2)
        {
            return (value - x1) / (y1 - x1) * (y2 - x2) + x2;
        }

        [Pure]
        public static int Map(int value, int x1, int y1, int x2, int y2)
        {
            return (value - x1) / (y1 - x1) * (y2 - x2) + x2;
        }
    }
}