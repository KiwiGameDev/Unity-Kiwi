using UnityEngine;

namespace Kiwi.Extensions
{
	public static class TransformExtensions
	{
		public static void Reset(this Transform transform)
		{
			transform.position = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}