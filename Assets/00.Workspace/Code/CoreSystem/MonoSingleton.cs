using UnityEngine;

namespace CoreSystem {
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T _instance;

        public static T Instance {
            get {
                if(_instance == null)
                    _instance = FindFirstObjectByType<T>();

                return _instance;
            }
        }

        protected virtual void Awake() {
            T[] managers = FindObjectsByType<T>(FindObjectsSortMode.None);
            if (managers.Length > 1)
                Destroy(gameObject);
        }

        protected void OnDestroy() {
            if(_instance == this)
                _instance = null;
        }
    }
}