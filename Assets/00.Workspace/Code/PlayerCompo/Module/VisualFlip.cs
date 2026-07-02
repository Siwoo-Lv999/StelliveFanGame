using UnityEngine;

namespace PlayerCompo {
    public class VisualFlip : MonoBehaviour, IPlayerModule {
        private Player _player;

        private void Start() {
            _player.GetModule<InputReceiver>().OnMoveInput += FlipX;
        }

        private void OnDestroy() {
            _player.GetModule<InputReceiver>().OnMoveInput -= FlipX;
        }

        private void FlipX(float moveDir) {
            if (moveDir == 0) return;
            
            if(moveDir > 0) {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                return;
            }

            if (moveDir < 0) {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                return;
            }
        }
        
        public void Initialize(Player player) {
            _player = player;
        }
    }
}