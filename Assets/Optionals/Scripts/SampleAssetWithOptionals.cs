using Kiwi.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "Kiwi/Sample Asset with Optionals")]
public class SampleAssetWithOptionals : ScriptableObject
{
    [SerializeField] Optional<int> testInt;
    [SerializeField] Optional<bool> testBool;
    [SerializeField] Optional<Vector2> testVector2;
    [SerializeField] Optional<Vector3> testVector3;
    [SerializeField] Optional<Vector4> testVector4;
}