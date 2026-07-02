using UnityEngine;

namespace PlayerCompo {
    public class PlayerJump : MonoBehaviour, IPlayerModule {
        [SerializeField] private Vector2 checkerSize;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private float maxJumpTime;
        [SerializeField] private float minJumpForce;
        [SerializeField] private float jumpForce;
        
        private bool _isGround;
        private float _startJumpTime;
        
        private Player _player;

        private void Start() {
            _player.GetModule<PlayerInput>().OnJumpStart += HandleJumpStart;
            _player.GetModule<PlayerInput>().OnJumpEnd += HandleJumpEnd;
        }

        private void OnDestroy() {
            _player.GetModule<PlayerInput>().OnJumpStart -= HandleJumpStart;
            _player.GetModule<PlayerInput>().OnJumpEnd -= HandleJumpEnd;
        }

        private void HandleJumpStart() {
            if(!_isGround) return;
            
            _startJumpTime = Time.time;
        }

        private void HandleJumpEnd() {
            if(!_isGround) return;
            
            float jumpTime = Time.time - _startJumpTime;
            float currentPercent = Mathf.Clamp(jumpTime, 0f, maxJumpTime);
            
            _player.RbCompo.AddForce((minJumpForce + jumpForce * currentPercent) * Vector2.up, ForceMode2D.Impulse);
        }

        private void FixedUpdate() {
            CheckGround();
        }

        private void CheckGround() {
            Collider2D checker = Physics2D.OverlapBox(transform.position, checkerSize, 0f, whatIsGround);
            _isGround = checker != null;
        }
        
        public void Initialize(Player player) {
            _player = player;
        }

        #region Gizmos

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, checkerSize);
        }

        #endregion
    }
}