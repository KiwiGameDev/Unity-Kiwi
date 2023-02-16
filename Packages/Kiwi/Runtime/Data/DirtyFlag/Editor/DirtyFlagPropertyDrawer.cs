#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Kiwi.Data.Editor
{
    [CustomPropertyDrawer(typeof(DirtyFlag))]
    public class DirtyFlagPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("value"));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("value");

            EditorGUI.PropertyField(position, valueProperty, label, true);
        }
    }
}
#endif
