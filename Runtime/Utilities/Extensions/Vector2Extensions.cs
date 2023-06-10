using UnityEngine;

namespace Kiwi.Utilities.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 GetMidPoint(this Vector2 a, Vector2 b)
        {
            return (a + b) * 0.5f;
        }
    }
}