using System;
using UnityEngine;

namespace Kiwi.Utilities.Extensions
{
    public static class RigidbodyExtensions
    {
        public static void ChangeDirection(this Rigidbody rigidbody, Vector3 direction)
        {
            Debug.Assert(Math.Abs(direction.sqrMagnitude - 1f) <= MoreMath.APPROXIMATION_TOLERANCE);

            rigidbody.velocity = direction * rigidbody.velocity.magnitude;
        }
    }
}