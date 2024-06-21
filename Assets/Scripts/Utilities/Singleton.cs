using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _awakeCalled = false;

        public static T Instance
        {
            get
            {
                if (_instance == null && _awakeCalled)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _awakeCalled = true;
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}