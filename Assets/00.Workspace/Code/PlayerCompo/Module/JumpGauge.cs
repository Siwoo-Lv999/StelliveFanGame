using UnityEngine;
using DG.Tweening;

namespace PlayerCompo {
    public class JumpGauge : MonoBehaviour, IPlayerModule {
        [SerializeField] private SpriteRenderer sr;

        private Vector2 minSize = new Vector2(1, 0);
        private Vector2 maxSize = new Vector2(1, 1);
        private float _startTime;
        private Tween _gaugeTween;
        
        private Player _player;

        private void Start() {
            sr.size = minSize;
            
            _player.GetModule<InputReceiver>().OnJumpStart += HandleJumpStart;
            _player.GetModule<InputReceiver>().OnJumpEnd += HandleJumpEnd;
        }

        private void OnDestroy() {
            _player.GetModule<InputReceiver>().OnJumpStart -= HandleJumpStart;
            _player.GetModule<InputReceiver>().OnJumpEnd -= HandleJumpEnd;
        }

        private void HandleJumpStart() {
            _gaugeTween = DOTween.To(() => sr.size, size => sr.size = size, maxSize, 1f);
        }

        private void HandleJumpEnd() {
            _gaugeTween.Kill();
            sr.size = minSize;
        }

        public void Initialize(Player player) {
            _player = player;
        }
    }
}