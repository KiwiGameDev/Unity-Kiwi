using UnityEngine;

namespace Kiwi.Core
{
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        void Awake()
        {
            thisInstance = GetComponent<T>();

            if (Instance == null)
            {
                Instance = thisInstance;

                OnSingletonAwake();
            }
            else if (thisInstance != Instance)
            {
                Destroy(gameObject);
            }
        }

        protected abstract void OnSingletonAwake();

        void OnDestroy()
        {
            if (Instance != thisInstance)
                return;

            OnSingletonDestroy();
		
            Instance = null;
        }

        protected abstract void OnSingletonDestroy();
	
        public static T Instance { get; private set; }

        T thisInstance;
    }
}