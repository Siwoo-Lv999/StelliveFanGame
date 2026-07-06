using DG.Tweening;
using UnityEngine;

namespace PlayerCompo {
    public class ChargeVisual : MonoBehaviour, IPlayerModule {
        private const string ChargeSquashId = "ChargeSquash";
        private const string RecoverSquashId = "RecoverSquash";
        
        [SerializeField] private float squashAmount = 0.06f;
        [SerializeField] private float squashTime = 1f;
        [SerializeField] private float recoverTime = 0.1f;
        
        private Player _player;
        
        //플레이어의 스케일이 바뀌면서 피봇의 위치가 바뀜 -> 점프 게이지가 땅 속으로 들어가게 됨
        //공중에서 UI 안보이게 개선해야함
        
        private void Start() {
            _player.GetModule<InputReceiver>().OnJumpStart += StartChangeVisual;
            _player.GetModule<InputReceiver>().OnJumpEnd += EndChangeVisual;
        }

        private void OnDestroy() {
            _player.GetModule<InputReceiver>().OnJumpStart -= StartChangeVisual;
            _player.GetModule<InputReceiver>().OnJumpEnd -= EndChangeVisual;
        }

        private void StartChangeVisual() {
            DOTween.Complete(RecoverSquashId);
            DOTween.Kill(ChargeSquashId);
            DOTween.Kill(RecoverSquashId);
            
            Vector3 visual = new Vector3(transform.localScale.x + squashAmount,
                transform.localScale.y - squashAmount, transform.localScale.z);
            
            transform.DOScale(visual, squashTime).SetEase(Ease.OutQuad).SetId(ChargeSquashId);
        }

        private void EndChangeVisual() {
            DOTween.Kill(RecoverSquashId);
            DOTween.Kill(ChargeSquashId);
            
            transform.DOScale(Vector3.one, recoverTime).SetId(RecoverSquashId);
        }

        public void Initialize(Player player) {
            _player = player;
        }
    }
}