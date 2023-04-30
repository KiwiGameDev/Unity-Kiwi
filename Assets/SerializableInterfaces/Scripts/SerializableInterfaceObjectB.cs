using System;
using UnityEngine;

namespace Kiwi.Data
{
    [Serializable]
    public class SerializableInterfaceObjectB : ISerializableInterfaceTest
    {
        [SerializeField] Vector2 value1 = new(1f, 1f);

        public void Test()
        {
            Debug.Log("I am a POCO!");
        }
    }
}