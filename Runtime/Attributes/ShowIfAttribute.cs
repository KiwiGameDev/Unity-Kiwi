using System;
using UnityEngine;

namespace Kiwi.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public readonly string[] Conditions;
        public readonly string EnumName;
        public readonly int EnumValueIndex;

        public ShowIfAttribute(params string[] conditions)
        {
            Conditions = conditions;
        }

        public ShowIfAttribute(string enumName, object enumValue)
        {
            EnumName = enumName;
            EnumValueIndex = (int) enumValue;
        }
    }
}
