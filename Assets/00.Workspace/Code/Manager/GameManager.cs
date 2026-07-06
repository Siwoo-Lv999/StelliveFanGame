using System;
using System.Collections.Generic;
using CoreSystem;
using PlayerCompo;
using UnityEngine;

namespace Manager {
    public class GameManager : MonoSingleton<GameManager> {
        private Dictionary<Type, MonoBehaviour> _gameModulesDict = new Dictionary<Type, MonoBehaviour>();

        protected override void Awake() {
            base.Awake();
        }

        public void SetMeModule(MonoBehaviour module) {
            Type type = module.GetType();
            if (_gameModulesDict == null)
                _gameModulesDict = new Dictionary<Type, MonoBehaviour>();
            
            if(!_gameModulesDict.TryAdd(type, module))
                Debug.LogWarning("Dictionary already have this Module");
        }

        public T GetGameModule<T>() where T : MonoBehaviour {
            Type type = typeof(T);

            if (_gameModulesDict.TryGetValue(type, out MonoBehaviour module))
                return module as T;

            return null;
        }
    }
}
