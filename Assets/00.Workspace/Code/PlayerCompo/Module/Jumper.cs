using UnityEngine;

namespace PlayerCompo {
    public class Jumper : MonoBehaviour, IPlayerModule {
        [SerializeField] private float maxJumpTime;
        [SerializeField] private float minJumpForce;
        [SerializeField] private float jumpForce;
        
        private float _startJumpTime;
        private bool _isJumping;
        
        private Player _player;
        private InputReceiver _input;

        private void Start() {
            _player.GetModule<InputReceiver>().OnJumpStart += HandleJumpStart;
            _player.GetModule<InputReceiver>().OnJumpEnd += HandleJumpEnd;

            _input = _player.GetModule<InputReceiver>();
        }

        private void OnDestroy() {
            _player.GetModule<InputReceiver>().OnJumpStart -= HandleJumpStart;
            _player.GetModule<InputReceiver>().OnJumpEnd -= HandleJumpEnd;
        }

        private void HandleJumpStart() {
            _isJumping = true;
            _startJumpTime = Time.time;
        }

        private void HandleJumpEnd() {
            if(!_isJumping) return;
            
            float jumpTime = Time.time - _startJumpTime;
            float currentPercent = Mathf.Clamp(jumpTime, 0f, maxJumpTime);
            
            _player.RbCompo.AddForce((minJumpForce + jumpForce * currentPercent) * Vector2.up, ForceMode2D.Impulse);
            
            _isJumping = false;
        }
        
        public void Initialize(Player player) {
            _player = player;
        }
    }
}