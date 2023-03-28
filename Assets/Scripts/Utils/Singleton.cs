using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance != null) return _instance;

                var singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<T>();
                singletonObject.name = typeof(T) + " (Singleton)";

                DontDestroyOnLoad(singletonObject);

                return _instance;
            }
        }
    }
}