#if UNITY_2019_3_OR_NEWER
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Kiwi.Data.Editor
{
    public class AdvancedTypePopupItem : AdvancedDropdownItem
    {
        public Type Type { get; }

        public AdvancedTypePopupItem (Type type,string name) : base(name) {
            Type = type;
        }
    }

    /// <summary>
    /// A type popup with a fuzzy finder.
    /// </summary>
    public class AdvancedTypePopup : AdvancedDropdown
    {
        const int MaxNamespaceNestCount = 16;

        static void AddTo(AdvancedDropdownItem root, IEnumerable<Type> types)
        {
            int itemCount = 0;

            // Add null item.
            AdvancedTypePopupItem nullItem = new(null,TypeMenuUtility.k_NullDisplayName)
            {
                id = itemCount++
            };
            root.AddChild(nullItem);

            Type[] typeArray = types.OrderByType().ToArray();

            // Single namespace if the root has one namespace and the nest is unbranched.
            bool isSingleNamespace = true;
            string[] namespaces = new string[MaxNamespaceNestCount];

            foreach (Type type in typeArray)
            {
                string[] splitTypePath = TypeMenuUtility.GetSplitTypePath(type);

                if (splitTypePath.Length <= 1)
                    continue;

                // If they explicitly want sub category, let them do.
                if (TypeMenuUtility.GetAttribute(type) != null)
                {
                    isSingleNamespace = false;
                    break;
                }

                for (int k = 0;splitTypePath.Length - 1 > k; k++)
                {
                    string ns = namespaces[k];

                    if (ns == null)
                    {
                        namespaces[k] = splitTypePath[k];
                    }
                    else if (ns != splitTypePath[k])
                    {
                        isSingleNamespace = false;
                        break;
                    }
                }

                if (!isSingleNamespace)
                    break;
            }

            // Add type items.
            foreach (Type type in typeArray)
            {
                string[] splitTypePath = TypeMenuUtility.GetSplitTypePath(type);

                if (splitTypePath.Length == 0)
                    continue;

                AdvancedDropdownItem parent = root;

                // Add namespace items.
                if (!isSingleNamespace)
                {
                    for (int k = 0; splitTypePath.Length - 1 > k; k++)
                    {
                        AdvancedDropdownItem foundItem = GetItem(parent,splitTypePath[k]);
                        if (foundItem != null)
                        {
                            parent = foundItem;
                        }
                        else {
                            AdvancedDropdownItem newItem = new(splitTypePath[k])
                            {
                                id = itemCount++,
                            };

                            parent.AddChild(newItem);
                            parent = newItem;
                        }
                    }
                }

                // Add type item.
                AdvancedTypePopupItem item = new(type, ObjectNames.NicifyVariableName(splitTypePath[^1]))
                {
                    id = itemCount++
                };

                parent.AddChild(item);
            }
        }

        static AdvancedDropdownItem GetItem(AdvancedDropdownItem parent, string name)
        {
            return parent.children.FirstOrDefault(item => item.name == name);
        }

        static readonly float HEADER_HEIGHT = EditorGUIUtility.singleLineHeight * 2f;

        Type[] types;

        public event Action<AdvancedTypePopupItem> OnItemSelected;

        public AdvancedTypePopup(IEnumerable<Type> types,int maxLineCount,AdvancedDropdownState state)
            : base(state)
        {
            SetTypes(types);
            minimumSize = new Vector2(minimumSize.x, EditorGUIUtility.singleLineHeight * maxLineCount + HEADER_HEIGHT);
        }

        void SetTypes(IEnumerable<Type> types)
        {
            this.types = types.ToArray();
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem root = new("Select Type");
            AddTo(root,types);

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item is AdvancedTypePopupItem typePopupItem)
            {
                OnItemSelected?.Invoke(typePopupItem);
            }
        }
    }
}
#endif