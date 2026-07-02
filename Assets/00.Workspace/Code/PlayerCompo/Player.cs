using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCompo {
    public class Player : MonoBehaviour, IDamageable {
        [field: SerializeField] public Rigidbody2D RbCompo { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public int CurrentHp { get; private set; }

        [SerializeField] private int maxHp = 3;
        
        private Dictionary<Type, IPlayerModule> _modulesDictionary;

        private void Awake() {
            CurrentHp = maxHp;
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

        public void GetDamage() {
            CurrentHp -= 1;
            //UI업데이트 이벤트 호출
        }
    }
}