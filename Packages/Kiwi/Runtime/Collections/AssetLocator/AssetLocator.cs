using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kiwi.Utils
{
	public abstract class AssetLocator<T> : ScriptableObject where T : Object
	{
		[Header("Editor")]
		[SerializeField] bool shouldRegenerate = true;

		[field: Header("Runtime")]
		[field: SerializeField] public List<T> Assets { get; private set; } = new();

#if UNITY_EDITOR
		void OnEnable()
		{
			AssetLocatorSaver.AssetProcessed += Regenerate;
		}

		[ContextMenu("Regenerate")]
		public void Regenerate()
		{
			if (!shouldRegenerate)
				return;

			List<T> foundAssets = AssetDatabase.FindAssets($"t: {typeof(T).FullName}")
				.Select(AssetDatabase.GUIDToAssetPath)
				.Select(AssetDatabase.LoadAssetAtPath<T>)
				.ToList();

			if (foundAssets.SequenceEqual(Assets))
				return;

			Assets = foundAssets;

			EditorUtility.SetDirty(this);
		}

		void OnDisable()
		{
			AssetLocatorSaver.AssetProcessed -= Regenerate;
		}
#endif
	}
}