using System.Collections.Generic;
using System.Reflection;
using Kiwi.Reflection.Extensions;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kiwi.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (CanEnable(property))
            {
                EditorGUI.PropertyField(position, property);
            }
            else
            {
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property);
                GUI.enabled = true;
            }
        }

        bool CanEnable(SerializedProperty serializedProperty)
        {
            SerializedObject serializedObject = serializedProperty.serializedObject;
            EnableIfAttribute enableIfAttribute = (EnableIfAttribute) attribute;

            if (enableIfAttribute.EnumName != null)
            {
                if (!EvaluateEnum(serializedObject, enableIfAttribute))
                    return false;
            }

            string[] conditions = enableIfAttribute.Conditions;

            if (conditions != null)
            {
                if (!EvaluatePredicates(conditions, serializedObject))
                    return false;
            }

            return true;
        }

        static bool EvaluateEnum(SerializedObject serializedObject, EnableIfAttribute showIfAttribute)
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