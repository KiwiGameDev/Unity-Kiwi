using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kiwi.Helpers
{
	public static class MoreApplication
	{
#if UNITY_EDITOR
		[InitializeOnEnterPlayMode]
		static void EnterPlayMode(EnterPlayModeOptions options)
		{
			IsQuitting = false;
		}
#endif

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
		static void RunOnStart()
		{
			IsQuitting = false;
			Application.quitting += Quit;
		}

		static void Quit()
		{
			IsQuitting = true;
		}

		public static bool IsQuitting { get; private set; }
	}
}
