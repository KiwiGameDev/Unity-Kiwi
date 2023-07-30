using System.Collections.Generic;
using System.Reflection;
using Kiwi.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kiwi.Attributes.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return CanShow(property) ? base.GetPropertyHeight(property, label) : 0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CanShow(property))
            {
                EditorGUI.PropertyField(position, property);
            }
        }

        bool CanShow(SerializedProperty serializedProperty)
        {
            SerializedObject serializedObject = serializedProperty.serializedObject;
            ShowIfAttribute showIfAttribute = (ShowIfAttribute) attribute;

            if (showIfAttribute.EnumName != null)
            {
                if (!EvaluateEnum(serializedObject, showIfAttribute))
                    return false;
            }

            string[] conditions = showIfAttribute.Conditions;

            if (conditions != null)
            {
                if (!EvaluatePredicates(conditions, serializedObject))
                    return false;
            }

            return true;
        }

        static bool EvaluateEnum(SerializedObject serializedObject, ShowIfAttribute showIfAttribute)
        {
            SerializedProperty conditionalSerializedProp = serializedObject.FindProperty(showIfAttribute.EnumName);
            int conditionalEnumValueIndex = conditionalSerializedProp.enumValueIndex;
            int showIfEnumValueIndex = showIfAttribute.EnumValueIndex;

            return conditionalEnumValueIndex == showIfEnumValueIndex;
        }

        static bool EvaluatePredicates(IEnumerable<string> conditions, SerializedObject serializedObject)
        {
            foreach (string conditionSerializePropName in conditions)
            {
                SerializedProperty conditionalSerializedProp = serializedObject.FindProperty(conditionSerializePropName);

                if (conditionalSerializedProp != null)
                {
                    if (conditionalSerializedProp.propertyType == SerializedPropertyType.Boolean)
                    {
                        bool conditionalValue = conditionalSerializedProp.boolValue;

                        if (!conditionalValue)
                            return false;
                    }
                    else
                    {
                        Debug.LogError($"{conditionalSerializedProp.propertyType} is not supported.");
                        return true; // Default to show on error
                    }
                }
                else
                {
                    Object target = serializedObject.targetObject;
                    MethodInfo conditionalMethodInfo = target.GetMethod(conditionSerializePropName);

                    if (!(bool) conditionalMethodInfo.Invoke(target, null))
                        return false;
                }
            }

            return true;
        }
    }
}
