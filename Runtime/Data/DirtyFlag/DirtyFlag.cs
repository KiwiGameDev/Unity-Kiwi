using System;
using UnityEngine;

namespace Kiwi.Data
{
    [Serializable]
    public class DirtyFlag
    {
        [SerializeField] bool value;

        public void SetValue(bool value = true)
        {
            this.value |= value;
        }

        public bool GetValue()
        {
            if (!value)
                return false;

            value = false;

            return true;
        }

        public bool GetValueNoReset()
        {
            return value;
        }
    }
}
