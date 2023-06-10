using UnityEngine;

namespace Kiwi.Utilities.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool IsPrefab(this GameObject gameObject)
        {
            return gameObject.scene.rootCount == 0;
        }
    }
}