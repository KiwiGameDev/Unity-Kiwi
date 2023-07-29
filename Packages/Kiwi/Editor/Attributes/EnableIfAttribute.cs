using System;
using UnityEngine;

namespace Kiwi.Editor.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class EnableIfAttribute : PropertyAttribute
    {
        public readonly string[] Conditions;
        public readonly string EnumName;
        public readonly int EnumValueIndex;

        public EnableIfAttribute(params string[] conditions)
        {
            Conditions = conditions;
        }

        public EnableIfAttribute(string enumName, object enumValue)
        {
            EnumName = enumName;
            EnumValueIndex = (int) enumValue;
        }
    }
}