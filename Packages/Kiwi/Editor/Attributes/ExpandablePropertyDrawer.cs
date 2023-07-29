using UnityEditor;
using UnityEngine;

namespace Kiwi.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(ExpandableAttribute))]
    public class ExpandablePropertyDrawer : PropertyDrawer
    {
        static readonly uint FOLDOUT_ARROW_WIDTH = 16;

        UnityEditor.Editor editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect propertyRect = position;
            propertyRect.xMin += FOLDOUT_ARROW_WIDTH;

            EditorGUI.PropertyField(propertyRect, property, label, true);

            if (property.propertyType != SerializedPropertyType.ObjectReference)
                return;
            if (property.objectReferenceValue == null)
                return;

            // ReSharper disable once AssignmentInConditionalExpression
            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(GUI.skin.box);

                if (editor == null)
                {
                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
                }

                editor.OnInspectorGUI();

                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }
        }
    }
}