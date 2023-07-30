using System;

namespace Kiwi.Editor
{
#if UNITY_EDITOR
    public abstract class TweakableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            OnBeforeDefaultInspector();
            DrawPropertiesExcluding(serializedObject, GetExcludedPropertiesInDefaultInspector());
            OnAfterDefaultInspector();
            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void OnBeforeDefaultInspector();
        protected abstract void OnAfterDefaultInspector();
        protected virtual string[] GetExcludedPropertiesInDefaultInspector() => emptyStringArray;

        static readonly string[] emptyStringArray = Array.Empty<string>();
    }
#endif
}