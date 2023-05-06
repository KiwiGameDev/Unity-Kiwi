using UnityEngine;
using UnityEditor;

namespace Kiwi.Editor
{
    [CustomEditor(typeof(BezierPoint3D), true)]
    [CanEditMultipleObjects]
    public class BezierPoint3DEditor : UnityEditor.Editor
    {
        public const float CircleCapSize = 0.075f;
        public const float RectangeCapSize = 0.1f;
        public const float SphereCapSize = 0.15f;

        public static float pointCapSize = RectangeCapSize;
        public static float handleCapSize = CircleCapSize;

        BezierPoint3D point;
        SerializedProperty handleType;
        SerializedProperty leftHandleLocalPosition;
        SerializedProperty rightHandleLocalPosition;

        protected virtual void OnEnable()
        {
            point = (BezierPoint3D)target;
            handleType = serializedObject.FindProperty("handleType");
            leftHandleLocalPosition = serializedObject.FindProperty("leftHandleLocalPosition");
            rightHandleLocalPosition = serializedObject.FindProperty("rightHandleLocalPosition");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(handleType);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(leftHandleLocalPosition);
            if (EditorGUI.EndChangeCheck())
            {
                rightHandleLocalPosition.vector3Value = -leftHandleLocalPosition.vector3Value;
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(rightHandleLocalPosition);
            if (EditorGUI.EndChangeCheck())
            {
                leftHandleLocalPosition.vector3Value = -rightHandleLocalPosition.vector3Value;
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnSceneGUI()
        {
            handleCapSize = CircleCapSize;
            BezierCurve3DEditor.DrawPointsSceneGUI(point.Curve, point);

            handleCapSize = SphereCapSize;
            DrawPointSceneGUI(point, Handles.DotHandleCap, Handles.SphereHandleCap);
        }

        public static void DrawPointSceneGUI(BezierPoint3D point)
        {
            DrawPointSceneGUI(point, Handles.RectangleHandleCap, Handles.CircleHandleCap);
        }

        public static void DrawPointSceneGUI(BezierPoint3D point, Handles.CapFunction drawPointFunc, Handles.CapFunction drawHandleFunc)
        {
            // Draw a label for the point
            Handles.color = Color.black;
            Handles.Label(point.Position + new Vector3(0f, HandleUtility.GetHandleSize(point.Position) * 0.4f, 0f), point.gameObject.name);

            // Draw the center of the control point
            Handles.color = Color.yellow;
            var fmh_75_79_638189778127669170 = point.transform.rotation; Vector3 newPointPosition = Handles.FreeMoveHandle(point.Position,
                HandleUtility.GetHandleSize(point.Position) * pointCapSize, Vector3.one * 0.5f, drawPointFunc);

            if (point.Position != newPointPosition)
            {
                Undo.RegisterCompleteObjectUndo(point.transform, "Move Point");
                point.Position = newPointPosition;
            }

            // Draw the left and right handles
            Handles.color = Color.white;
            Handles.DrawLine(point.Position, point.LeftHandlePosition);
            Handles.DrawLine(point.Position, point.RightHandlePosition);

            Handles.color = Color.cyan;
            var fmh_90_94_638189778127698090 = point.transform.rotation; Vector3 newLeftHandlePosition = Handles.FreeMoveHandle(point.LeftHandlePosition,
                HandleUtility.GetHandleSize(point.LeftHandlePosition) * handleCapSize, Vector3.zero, drawHandleFunc);

            if (point.LeftHandlePosition != newLeftHandlePosition)
            {
                Undo.RegisterCompleteObjectUndo(point, "Move Left Handle");
                point.LeftHandlePosition = newLeftHandlePosition;
            }

            var fmh_99_96_638189778127701068 = point.transform.rotation; Vector3 newRightHandlePosition = Handles.FreeMoveHandle(point.RightHandlePosition,
                HandleUtility.GetHandleSize(point.RightHandlePosition) * handleCapSize, Vector3.zero, drawHandleFunc);

            if (point.RightHandlePosition != newRightHandlePosition)
            {
                Undo.RegisterCompleteObjectUndo(point, "Move Right Handle");
                point.RightHandlePosition = newRightHandlePosition;
            }
        }

        static bool MouseButtonDown(int button)
        {
            return Event.current.type == EventType.MouseDown && Event.current.button == button;
        }

        static bool MouseButtonUp(int button)
        {
            return Event.current.type == EventType.MouseUp && Event.current.button == button;
        }
    }
}