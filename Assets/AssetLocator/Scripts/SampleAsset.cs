using Kiwi.Data;
using UnityEngine;

[CreateAssetMenu(menuName = "SampleAsset")]
public class SampleAsset : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Optional<Vector3> Position { get; private set; }
}