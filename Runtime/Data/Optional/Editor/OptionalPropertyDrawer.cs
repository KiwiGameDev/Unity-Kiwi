#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Kiwi.Data.Editor
{
    [CustomPropertyDrawer(typeof(Optional<>))]
    public class OptionalPropertyDrawer : PropertyDrawer
    {
        static readonly uint ENABLED_PROPERTY_WIDTH = 24;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("<Value>k__BackingField"));
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty valueProperty = property.FindPropertyRelative("<Value>k__BackingField");
            SerializedProperty enabledProperty = property.FindPropertyRelative("<Enabled>k__BackingField");

            position.width -= ENABLED_PROPERTY_WIDTH;
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            EditorGUI.PropertyField(position, valueProperty, label, true);
            EditorGUI.EndDisabledGroup();

            position.x += position.width + ENABLED_PROPERTY_WIDTH;
            position.width = EditorGUI.GetPropertyHeight(enabledProperty);
            position.height = EditorGUI.GetPropertyHeight(enabledProperty);
            position.x -= position.width;

            EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
        }
    }
}
#endif