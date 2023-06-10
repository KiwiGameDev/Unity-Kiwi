using UnityEngine;

namespace Kiwi.Utilities.Extensions
{
    public static class CameraExtensions
    {
        public static bool IsObjectVisible(this Camera camera, Renderer renderer)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
        }
    }
}