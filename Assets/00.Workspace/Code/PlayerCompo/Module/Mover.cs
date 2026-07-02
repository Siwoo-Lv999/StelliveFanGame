using System;
using UnityEngine;

namespace PlayerCompo {
    public class Mover : MonoBehaviour, IPlayerModule {
        private float _moveDir;
        private bool _canMove = true;
        
        private Player _player;

        private void Start() {
            _player.GetModule<InputReceiver>().OnMoveInput += SetMoveDir;
        }

        private void OnDestroy() {
            _player.GetModule<InputReceiver>().OnMoveInput -= SetMoveDir;
        }

        public void Initialize(Player player) {
            _player = player;
        }
        
        private void FixedUpdate() {
            Move();
        }

        private void Move() {
            if (!_canMove) return;
            _player.RbCompo.linearVelocityX = _moveDir * _player.MoveSpeed;
        }

        private void SetMoveDir(float moveDir) {
            _moveDir = moveDir;
        }

        private void CanMove(bool canMove) {
            _canMove = canMove;
        }
    }
}