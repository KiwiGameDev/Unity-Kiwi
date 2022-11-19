#if UNITY_EDITOR
using System;
using UnityEditor;

namespace Kiwi.Data.Automation.Editor
{
	class AssetLocatorSaver : AssetPostprocessor
	{
		internal static event Action AssetProcessed;

		static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths, bool didDomainReload)
		{
			AssetProcessed?.Invoke();
		}
	}
}

#endif