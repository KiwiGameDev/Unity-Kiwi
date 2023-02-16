using Kiwi.Data;
using UnityEngine;

[DisallowMultipleComponent]
public class DirtyFlagTester : MonoBehaviour
{
    [Header("Test Values")]
    [SerializeField] DirtyFlag dirtyFlagA;
    [SerializeField] DirtyFlag dirtyFlagB;
    [SerializeField] DirtyFlag dirtyFlagC;
    [SerializeField] DirtyFlag dirtyFlagWithVeryLongNameA;

    [ContextMenu("Set Value A")]
    void SetValue1()
    {
        dirtyFlagA.SetValue();
    }

    [ContextMenu("Get & Log Value A")]
    void GetAndLogValue1()
    {
        Debug.Log(dirtyFlagA.GetValue());
    }

    [ContextMenu("Get & Log Value A - No Reset")]
    void GetAndLogValue1NoReset()
    {
        Debug.Log(dirtyFlagA.GetValueNoReset());
    }
}
