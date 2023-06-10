using UnityEngine;

namespace Kiwi.Utilities
{
    public static class MoreGizmos
    {
        public static void DrawBoxXY(Vector3 origin, Vector2 extents)
        {
            float z = origin.z;
            Vector3 tl = new(origin.x - extents.x, origin.y + extents.y, z);
            Vector3 tr = new(origin.x + extents.x, origin.y + extents.y, z);
            Vector3 bl = new(origin.x - extents.x, origin.y - extents.y, z);
            Vector3 br = new(origin.x + extents.x, origin.y - extents.y, z);

            Gizmos.DrawLine(tl, tr);
            Gizmos.DrawLine(bl, br);
            Gizmos.DrawLine(tl, bl);
            Gizmos.DrawLine(tr, br);
        }
    }
}