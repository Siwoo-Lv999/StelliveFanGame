using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerCompo {
    public class InputReceiver : MonoBehaviour, IPlayerModule, Controls.IPlayerActions {
        public event Action<float> OnMoveInput;
        public event Action OnJumpStart;
        public event Action OnJumpEnd;

        private float _chargeStartTime;
        
        private Controls _controls;
        private Player _player;
        private ContactChecker _checker;

        private void Awake() {
            _controls = new Controls();
            _controls.Enable();
            _controls.Player.SetCallbacks(this);
        }

        private void Start() {
            _checker = _player.GetModule<ContactChecker>();
        }

        private void OnDestroy() {
            _controls.Disable();
            _controls = null;
        }

        public void OnMove(InputAction.CallbackContext context) {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>().x);
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.started && _checker.IsGround)
                OnJumpStart?.Invoke();
            

            if (context.canceled) 
                OnJumpEnd?.Invoke();
        }

        public void Initialize(Player player) {
            _player = player;
        }
    }
}