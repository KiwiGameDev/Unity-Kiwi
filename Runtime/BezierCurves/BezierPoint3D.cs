using UnityEngine;

namespace Kiwi
{
    public class BezierPoint3D : MonoBehaviour
    {
        public enum HandleType
        {
            Connected,
            Broken
        }

        // Serializable Fields
        [Tooltip("The curve that the point belongs to")]
        [SerializeField] BezierCurve3D curve;
        [SerializeField] HandleType handleType = HandleType.Connected;
        [SerializeField] Vector3 leftHandleLocalPosition = new(-0.5f, 0f, 0f);
        [SerializeField] Vector3 rightHandleLocalPosition = new(0.5f, 0f, 0f);

        /// <summary>
        /// Gets or sets the curve that the point belongs to.
        /// </summary>
        public BezierCurve3D Curve { get => curve; set => curve = value; }

        /// <summary>
        /// Gets or sets the type/style of the handle.
        /// </summary>
        public HandleType HandleStyle { get => handleType; set => handleType = value; }

        /// <summary>
        /// Gets or sets the position of the transform.
        /// </summary>
        public Vector3 Position { get => transform.position; set => transform.position = value; }

        /// <summary>
        /// Gets or sets the position of the transform.
        /// </summary>
        public Vector3 LocalPosition { get => transform.localPosition; set => transform.localPosition = value; }

        /// <summary>
        /// Gets or sets the local position of the left handle.
        /// If the HandleStyle is Connected, the local position of the right handle is automatically set.
        /// </summary>
        public Vector3 LeftHandleLocalPosition
        {
            get => leftHandleLocalPosition;
            set
            {
                leftHandleLocalPosition = value;

                if (handleType == HandleType.Connected)
                {
                    rightHandleLocalPosition = -value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the local position of the right handle.
        /// If the HandleType is Connected, the local position of the left handle is automatically set.
        /// </summary>
        public Vector3 RightHandleLocalPosition
        {
            get => rightHandleLocalPosition;
            set
            {
                rightHandleLocalPosition = value;

                if (handleType == HandleType.Connected)
                {
                    leftHandleLocalPosition = -value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of the left handle.
        /// If the HandleStyle is Connected, the position of the right handle is automatically set.
        /// </summary>
        public Vector3 LeftHandlePosition
        {
            get => transform.TransformPoint(LeftHandleLocalPosition);
            set => LeftHandleLocalPosition = transform.InverseTransformPoint(value);
        }

        /// <summary>
        /// Gets or sets the position of the right handle.
        /// If the HandleType is Connected, the position of the left handle is automatically set.
        /// </summary>
        public Vector3 RightHandlePosition
        {
            get => transform.TransformPoint(RightHandleLocalPosition);
            set => RightHandleLocalPosition = transform.InverseTransformPoint(value);
        }
    }
}
