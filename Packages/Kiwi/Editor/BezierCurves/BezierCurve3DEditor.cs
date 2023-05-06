using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Kiwi.Editor
{
    [CustomEditor(typeof(BezierCurve3D))]
    [CanEditMultipleObjects]
    public class BezierCurve3DEditor : UnityEditor.Editor
    {
        const float AddButtonWidth = 80f;
        const float RemoveButtonWidth = 19f;

        BezierCurve3D curve;
        ReorderableList keyPoints;
        bool showPoints = true;

        [MenuItem("GameObject/Create Other/Naughty Bezier Curve")]
        static void CreateBezierCurve()
        {
            BezierCurve3D curve = new GameObject("Bezier Curve", typeof(BezierCurve3D)).GetComponent<BezierCurve3D>();
            Vector3 position = Vector3.zero;
            Camera currentCamera = Camera.current;

            if (currentCamera != null)
            {
                position = currentCamera.transform.position + currentCamera.transform.forward * 10f;
            }

            curve.transform.position = position;

            AddDefaultPoints(curve);
            Undo.RegisterCreatedObjectUndo(curve.gameObject, "Create Curve");
            Selection.activeGameObject = curve.gameObject;
        }

        static void AddDefaultPoints(BezierCurve3D curve)
        {
            BezierPoint3D startPoint = curve.AddKeyPoint();
            startPoint.LocalPosition = new Vector3(-1f, 0f, 0f);
            startPoint.LeftHandleLocalPosition = new Vector3(-0.35f, -0.35f, 0f);

            BezierPoint3D endPoint = curve.AddKeyPoint();
            endPoint.LocalPosition = new Vector3(1f, 0f, 0f);
            endPoint.LeftHandleLocalPosition = new Vector3(-0.35f, 0.35f, 0f);
        }

        protected virtual void OnEnable()
        {
            curve = (BezierCurve3D) target;
            if (curve.KeyPointsCount < 2)
            {
                while (curve.KeyPointsCount != 0)
                {
                    curve.RemoveKeyPointAt(curve.KeyPointsCount - 1);
                }

                AddDefaultPoints(curve);
            }

            keyPoints = new ReorderableList(serializedObject, serializedObject.FindProperty("keyPoints"), true, true, false, false);
            keyPoints.drawElementCallback = DrawElementCallback;
            keyPoints.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, string.Format("Points: {0}", keyPoints.serializedProperty.arraySize), EditorStyles.boldLabel);
            };
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            if (GUILayout.Button("Log Length"))
            {
                Debug.Log(curve.GetApproximateLength());
            }

            showPoints = EditorGUILayout.Foldout(showPoints, "Key Points");
            if (showPoints)
            {
                if (GUILayout.Button("Add Point"))
                {
                    AddKeyPointAt(curve, curve.KeyPointsCount);
                }

                if (GUILayout.Button("Add Point and Select"))
                {
                    var point = AddKeyPointAt(curve, curve.KeyPointsCount);
                    Selection.activeGameObject = point.gameObject;
                }

                keyPoints.DoLayoutList();
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void OnSceneGUI()
        {
            DrawPointsSceneGUI(curve);
        }

        void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = keyPoints.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            // Draw "Add Before" button
            if (GUI.Button(new Rect(rect.x, rect.y, AddButtonWidth, EditorGUIUtility.singleLineHeight), new GUIContent("Add Before")))
            {
                AddKeyPointAt(curve, index);
            }

            // Draw point name
            EditorGUI.PropertyField(
                new Rect(rect.x + AddButtonWidth + 5f, rect.y, rect.width - AddButtonWidth * 2f - 35f, EditorGUIUtility.singleLineHeight), element, GUIContent.none);

            // Draw "Add After" button
            if (GUI.Button(new Rect(rect.width - AddButtonWidth + 8f, rect.y, AddButtonWidth, EditorGUIUtility.singleLineHeight), new GUIContent("Add After")))
            {
                AddKeyPointAt(curve, index + 1);
            }

            // Draw remove button
            if (curve.KeyPointsCount > 2)
            {
                if (GUI.Button(new Rect(rect.width + 14f, rect.y, RemoveButtonWidth, EditorGUIUtility.singleLineHeight), new GUIContent("x")))
                {
                    RemoveKeyPointAt(curve, index);
                }
            }
        }

        public static void DrawPointsSceneGUI(BezierCurve3D curve, BezierPoint3D exclude = null)
        {
            for (int i = 0; i < curve.KeyPointsCount; i++)
            {
                if (curve.KeyPoints[i] == exclude)
                    continue;

                BezierPoint3DEditor.handleCapSize = BezierPoint3DEditor.CircleCapSize;
                BezierPoint3DEditor.DrawPointSceneGUI(curve.KeyPoints[i]);
            }
        }

        static void RenamePoints(BezierCurve3D curve)
        {
            for (int i = 0; i < curve.KeyPointsCount; i++)
            {
                curve.KeyPoints[i].name = "Point " + i;
            }
        }

        static BezierPoint3D AddKeyPointAt(BezierCurve3D curve, int index)
        {
            BezierPoint3D newPoint = new GameObject("Point " + curve.KeyPointsCount, typeof(BezierPoint3D)).GetComponent<BezierPoint3D>();
            Transform newPointTransform = newPoint.transform;
            newPointTransform.parent = curve.transform;
            newPointTransform.localRotation = Quaternion.identity;
            newPoint.Curve = curve;

            if (curve.KeyPointsCount == 0 || curve.KeyPointsCount == 1)
            {
                newPoint.LocalPosition = Vector3.zero;
            }
            else
            {
                if (index == 0)
                {
                    newPoint.Position = (curve.KeyPoints[0].Position - curve.KeyPoints[1].Position).normalized + curve.KeyPoints[0].Position;
                }
                else if (index == curve.KeyPointsCount)
                {
                    newPoint.Position = (curve.KeyPoints[index - 1].Position - curve.KeyPoints[index - 2].Position).normalized + curve.KeyPoints[index - 1].Position;
                }
                else
                {
                    newPoint.Position = BezierCurve3D.GetPointOnCubicCurve(0.5f, curve.KeyPoints[index - 1], curve.KeyPoints[index]);
                }
            }

            Undo.IncrementCurrentGroup();
            Undo.RegisterCreatedObjectUndo(newPoint.gameObject, "Create Point");
            Undo.RegisterCompleteObjectUndo(curve, "Save Curve");

            curve.KeyPoints.Insert(index, newPoint);
            RenamePoints(curve);

            //Undo.RegisterCompleteObjectUndo(curve, "Save Curve");

            return newPoint;
        }

        static bool RemoveKeyPointAt(BezierCurve3D curve, int index)
        {
            if (curve.KeyPointsCount < 2)
            {
                return false;
            }

            var point = curve.KeyPoints[index];

            Undo.IncrementCurrentGroup();
            Undo.RegisterCompleteObjectUndo(curve, "Save Curve");

            curve.KeyPoints.RemoveAt(index);
            RenamePoints(curve);

            //Undo.RegisterCompleteObjectUndo(curve, "Save Curve");
            Undo.DestroyObjectImmediate(point.gameObject);

            return true;
        }
    }
}
