#if UNITY_2019_3_OR_NEWER
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Kiwi.Data.Editor
{
    public static class TypeMenuUtility
    {
        public const string k_NullDisplayName = "<null>";

        public static AddTypeMenuAttribute GetAttribute(Type type)
        {
            return Attribute.GetCustomAttribute(type, typeof(AddTypeMenuAttribute)) as AddTypeMenuAttribute;
        }

        public static string[] GetSplitTypePath(Type type)
        {
            AddTypeMenuAttribute typeMenu = GetAttribute(type);

            if (typeMenu != null)
                return typeMenu.GetSplitMenuName();

            Debug.Assert(type.FullName != null);

            int splitIndex = type.FullName.LastIndexOf('.');

            return splitIndex >= 0
                ? new[] { type.FullName.Substring(0,splitIndex), type.FullName.Substring(splitIndex + 1) }
                : new[] { type.Name };
        }

        public static IEnumerable<Type> OrderByType(this IEnumerable<Type> source)
        {
            return source.OrderBy(type =>
            {
                if (type == null)
                    return -999;

                return GetAttribute(type)?.Order ?? 0;
            }).ThenBy(type =>
            {
                if (type == null)
                    return null;

                return GetAttribute(type)?.MenuName ?? type.Name;
            });
        }
    }
}
#endif