using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCompo {
    public class Player : MonoBehaviour {
        [field: SerializeField] public Rigidbody2D RbCompo { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        

        private Dictionary<Type, IPlayerModule> _modulesDictionary;

        private void Awake() {
            _modulesDictionary = new Dictionary<Type, IPlayerModule>();
            
            IPlayerModule[] modules = GetComponentsInChildren<IPlayerModule>();

            foreach (IPlayerModule module in modules) {
                Type type = module.GetType();
                _modulesDictionary.Add(type, module);
                module.Initialize(this);
            }
        }

        public T GetModule<T>() where T : class, IPlayerModule {
            Type type = typeof(T);

            if (_modulesDictionary.TryGetValue(type, out IPlayerModule module))
                return module as T;
            
            return null;
        }
    }
}