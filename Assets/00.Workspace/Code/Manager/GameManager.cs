using CoreSystem;
using PlayerCompo;
using UnityEngine;

namespace Manager {
    public class GameManager : MonoSingleton<GameManager> {
        [field: SerializeField] public Player Player { get; private set; }
        public void SetPlayer(Player player) => Player = player;
    }
}