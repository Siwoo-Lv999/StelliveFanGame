using UnityEngine;
using UnityEngine.Events;

namespace BossCompo {
    [CreateAssetMenu(fileName = "AttackData", menuName = "SO/Boss/AttackData", order = 0)]
    public class BossAttackSO : ScriptableObject {
        public string attackName;
        public float attackDelay;
        public UnityEvent onAttackStart;
        public GameObject attackPrefab;
        
        public BossController boss;

        public void Init(BossController bossController) {
            boss = bossController;
        }
    }
}