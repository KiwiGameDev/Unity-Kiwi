using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "Letter")]
public class SampleAsset : ScriptableObject
{
	[field: SerializeField] public string Name { get; private set; }
}
