using UnityEngine;

namespace Kiwi.Extensions
{
    public static class TouchExtensions
    {
        public static Vector2 GetPreviousPoint(this Touch touch)
        {
            return touch.position - touch.deltaPosition;
        }

        public static float Distance(Touch finger1, Touch finger2)
        {
            return Vector2.Distance(finger1.position, finger2.position);
        }
    }
}