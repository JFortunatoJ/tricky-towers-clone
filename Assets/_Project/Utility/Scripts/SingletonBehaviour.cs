using UnityEngine;

namespace MiniclipTrick.Utility
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance != null) return _instance;

                var instanceName = $"_{typeof(T)}";
                var go = new GameObject(instanceName);
                _instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                SetupInstance();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void SetupInstance()
        {
        }
    }
}