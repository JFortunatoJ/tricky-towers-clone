using UnityEngine;

namespace MiniclipTest.Utility
{
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance is null) return _instance;

                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance is not null) return _instance;

                var instanceName = $"_{typeof(T)}";
                var go = new GameObject(instanceName);
                _instance = go.AddComponent<T>();
                DontDestroyOnLoad(go);

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
            SetupInstance();
        }

        protected virtual void SetupInstance()
        {
        }

        protected virtual void OnApplicationQuit()
        {
            _instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            base.Awake();
        }
    }

    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}