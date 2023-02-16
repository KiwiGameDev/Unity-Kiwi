using UnityEngine;

namespace Kiwi.Extensions
{
    public static class RigidbodyExtensions
    {
        public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
        {
            rigidbody.velocity = direction * rigidbody.velocity.magnitude;
        }
    }
}