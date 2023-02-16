using System.Collections.Generic;
using UnityEngine;

namespace Kiwi.Extensions
{
    public static class Vector3Extensions
    {
        public static float AngleOffAroundAxis(Vector3 to, Vector3 from, Vector3 axis, bool clockwise = false)
        {
            Vector3 right;

            if (clockwise)
            {
                right = Vector3.Cross(from, axis);
                from = Vector3.Cross(axis, right);
            }
            else
            {
                right = Vector3.Cross(axis, from);
                from = Vector3.Cross(right, axis);
            }

            return Mathf.Atan2(Vector3.Dot(to, right), Vector3.Dot(to, from)) * Mathf.Rad2Deg;
        }

        public static Vector3 GetClosest(this Vector3 position, IEnumerable<Vector3> otherPositions)
        {
            Vector3 closest = Vector3.zero;
            float shortestDistance = Mathf.Infinity;

            foreach (Vector3 otherPosition in otherPositions)
            {
                var distance = (position - otherPosition).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    closest = otherPosition;
                    shortestDistance = distance;
                }
            }

            return closest;
        }

        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }

        public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
        {
            return Vector3.Normalize(destination - source);
        }

        public static Vector3 Rescale(this Vector3 original, float scale)
        {
            return original.normalized * scale;
        }

        public static float SqrDistanceTo(this Vector3 source, Vector3 destination)
        {
            return (destination - source).sqrMagnitude;
        }

        public static Vector3 GetRandomFromRange(this Vector3 randomRange)
        {
            return new Vector3
            (
                Random.Range(-randomRange.x, randomRange.x),
                Random.Range(-randomRange.y, randomRange.y),
                Random.Range(-randomRange.z, randomRange.z)
            );
        }
    }
}