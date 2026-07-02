using UnityEngine;

namespace PlayerCompo {
    public class ContactChecker : MonoBehaviour, IPlayerModule {
        [SerializeField] private Vector2 checkerSize;
        [SerializeField] private LayerMask whatIsGround;
        
        public bool IsGround { get; private set; }
        
        private Player _player;
        
        private void FixedUpdate() {
            CheckGround();
        }
        
        private void CheckGround() {
            Collider2D checker = Physics2D.OverlapBox(transform.position, checkerSize, 0f, whatIsGround);
            IsGround = checker != null;
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