using System;
using UnityEngine;

namespace PlayerCompo {
    public class HitBox : MonoBehaviour, IPlayerModule {
        private Player _player;

        private void OnTriggerEnter2D(Collider2D collision) {
            
        }

        public void Initialize(Player player) {
            _player = player;
        }
    }
}