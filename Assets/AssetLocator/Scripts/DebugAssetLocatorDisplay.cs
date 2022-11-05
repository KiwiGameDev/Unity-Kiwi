using Kiwi.Utils;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class DebugAssetLocatorDisplay : MonoBehaviour
{
	[SerializeField] AssetLocator<SampleAsset> assetLocator;
	[SerializeField] TMP_Text assetsCountText;

	void Start()
	{
		assetsCountText.text = string.Empty;
		assetsCountText.text = assetLocator.Assets.Count.ToString();
		assetsCountText.text += '\n';

		foreach (SampleAsset sampleAsset in assetLocator.Assets)
		{
			assetsCountText.text += sampleAsset.Name;
			assetsCountText.text += '\n';
		}
	}
}
