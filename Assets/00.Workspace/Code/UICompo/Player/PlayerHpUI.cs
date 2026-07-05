using System.Collections.Generic;
using Manager;
using PlayerCompo;
using UnityEngine;

namespace PlayerUI {
    public class PlayerHpUI : MonoBehaviour {
        [SerializeField] private GameObject hpImagePrefab;

        private List<GameObject> _hpList = new List<GameObject>();

        private Player _player;

        private void Start() {
            _player = GameManager.Instance.GetGameModule<Player>();
            
            for (int i = 0; i < _player.CurrentHp; i++) {
                GameObject hpImage = Instantiate(hpImagePrefab, transform);
                _hpList.Add(hpImage);
            }
            _player.OnDamaged += HandleDamaged;
        }

        private void OnDestroy() {
            _player.OnDamaged -= HandleDamaged;
        }

        private void HandleDamaged() {
            foreach (GameObject hpImage in _hpList) {
                Destroy(hpImage);
            }
            _hpList.Clear();

            for (int i = 0; i < _player.CurrentHp; i++) {
                GameObject hpImage = Instantiate(hpImagePrefab, transform);
                _hpList.Add(hpImage);
            }
        }
    }
}