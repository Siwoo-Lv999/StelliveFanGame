using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCompo {
    public class Player : MonoBehaviour, IDamageable {
        [field: SerializeField] public Rigidbody2D RbCompo { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public int CurrentHp { get; private set; }

        [SerializeField] private int maxHp = 3;
        [SerializeField] private float getDamageCooldownTime = 1f;
        
        private Dictionary<Type, IPlayerModule> _modulesDictionary;
        private bool _canGetDamage = true;
        private WaitForSeconds _getDamageCooldown;

        private void Awake() {
            CurrentHp = maxHp;
            _getDamageCooldown = new WaitForSeconds(getDamageCooldownTime);
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
            if (!_canGetDamage) return;
            
            CurrentHp -= 1;
            StartCoroutine(GetDamageCooldown());

            //UI업데이트 이벤트 호출
        }

        private IEnumerator GetDamageCooldown() {
            yield return _getDamageCooldown;
            _canGetDamage = true;
        }
    }
}