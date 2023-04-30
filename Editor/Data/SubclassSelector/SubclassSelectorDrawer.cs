#if UNITY_2019_3_OR_NEWER
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Kiwi.Data.Editor
{
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer
    {
        struct TypePopupCache
        {
            public AdvancedTypePopup TypePopup { get; }
            public AdvancedDropdownState State { get; }

            public TypePopupCache (AdvancedTypePopup typePopup, AdvancedDropdownState state)
            {
                TypePopup = typePopup;
                State = state;
            }
        }

        const int MaxTypePopupLineCount = 13;
        static readonly Type UNITY_OBJECT_TYPE = typeof(UnityEngine.Object);
        static readonly GUIContent NULL_DISPLAY_NAME = new(TypeMenuUtility.k_NullDisplayName);
        static readonly GUIContent IS_NOT_MANAGED_REFERENCE_LABEL = new("The property type is not manage reference.");

        readonly Dictionary<string,TypePopupCache> typePopups = new();
        readonly Dictionary<string,GUIContent> typeNameCaches = new();

        SerializedProperty targetProperty;

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.ManagedReference)
            {
                // Draw the subclass selector popup.
                Rect popupPosition = new(position);
                popupPosition.width -= EditorGUIUtility.labelWidth;
                popupPosition.x += EditorGUIUtility.labelWidth;
                popupPosition.height = EditorGUIUtility.singleLineHeight;

                if (EditorGUI.DropdownButton(popupPosition,GetTypeName(property),FocusType.Keyboard))
                {
                    TypePopupCache popup = GetTypePopup(property);
                    targetProperty = property;
                    popup.TypePopup.Show(popupPosition);
                }

                // Draw the managed reference property.
                EditorGUI.PropertyField(position, property, label, true);
            }
            else
            {
                EditorGUI.LabelField(position, label, IS_NOT_MANAGED_REFERENCE_LABEL);
            }

            EditorGUI.EndProperty();
        }

        TypePopupCache GetTypePopup(SerializedProperty property)
        {
            // Cache this string. This property internally call Assembly.GetName, which result in a large allocation.
            string managedReferenceFieldTypename = property.managedReferenceFieldTypename;

            if (typePopups.TryGetValue(managedReferenceFieldTypename, out TypePopupCache result))
                return result;

            AdvancedDropdownState state = new();
            Type baseType = ManagedReferenceUtility.GetType(managedReferenceFieldTypename);
            AdvancedTypePopup popup = new
            (
                TypeCache.GetTypesDerivedFrom(baseType)
                    .Append(baseType)
                    .Where(p =>
                        (p.IsPublic || p.IsNestedPublic) &&
                        !p.IsAbstract &&
                        !p.IsGenericType &&
                        !UNITY_OBJECT_TYPE.IsAssignableFrom(p) &&
                        Attribute.IsDefined(p,typeof(SerializableAttribute)
                    )
                ),
                MaxTypePopupLineCount,
                state
            );
            popup.OnItemSelected += item =>
            {
                Type type = item.Type;
                object obj = targetProperty.SetManagedReference(type);
                targetProperty.isExpanded = obj != null;
                targetProperty.serializedObject.ApplyModifiedProperties();
                targetProperty.serializedObject.Update();
            };
            result = new TypePopupCache(popup, state);
            typePopups.Add(managedReferenceFieldTypename, result);

            return result;
        }

        GUIContent GetTypeName(SerializedProperty property)
        {
            // Cache this string.
            string managedReferenceFullTypename = property.managedReferenceFullTypename;

            if (string.IsNullOrEmpty(managedReferenceFullTypename))
                return NULL_DISPLAY_NAME;
            if (typeNameCaches.TryGetValue(managedReferenceFullTypename, out GUIContent cachedTypeName))
                return cachedTypeName;

            Type type = ManagedReferenceUtility.GetType(managedReferenceFullTypename);
            string typeName = null;
            AddTypeMenuAttribute typeMenu = TypeMenuUtility.GetAttribute(type);

            if (typeMenu != null)
            {
                typeName = typeMenu.GetTypeNameWithoutPath();

                if (!string.IsNullOrWhiteSpace(typeName))
                {
                    typeName = ObjectNames.NicifyVariableName(typeName);
                }
            }

            if (string.IsNullOrWhiteSpace(typeName))
            {
                typeName = ObjectNames.NicifyVariableName(type.Name);
            }

            GUIContent result = new(typeName);
            typeNameCaches.Add(managedReferenceFullTypename, result);

            return result;
        }

        public override float GetPropertyHeight(SerializedProperty property,GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property,true);
        }
    }
}
#endif