using System.Collections.Generic;
using UnityEngine;

namespace Kiwi.Data
{
    [DisallowMultipleComponent]
    public class SerializableInterfacesTester : MonoBehaviour
    {
        [SerializeReference, SubclassSelector] ISerializableInterfaceTest serializableInterfaceTest;
        [SerializeReference, SubclassSelector] List<ISerializableInterfaceTest> serializableInterfaceTestList;
    }
}
