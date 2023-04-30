using System;
using UnityEngine;

namespace Kiwi.Data
{
    [Serializable]
    public class SerializableInterfaceObjectA : ISerializableInterfaceTest
    {
        [SerializeField, Min(0)] float value1 = 1f;

        public void Test()
        {
            Debug.Log("I am a POCO!");
        }
    }
}