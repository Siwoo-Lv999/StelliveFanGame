using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace SpawnManager {
    public class PlatformSpawnManager : MonoBehaviour {
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private ParticleSystem destroyParticle;

        private List<GameObject> _platforms;

        private void Awake() {
            _platforms = new List<GameObject>();
        }

        [ContextMenu("Spawn Platform")]
        private void HandlePlatformSpawn() {
            foreach (var point in spawnPoint) {
                GameObject platform = Instantiate(platformPrefab, point);
                platform.transform.position = point.position + Vector3.up * 20;
                platform.transform.DOMove(point.position, 1f);
                _platforms.Add(platform);
            }
        }

        [ContextMenu("Despawn Platform")]
        private void HandlePlatformDestroy() {
            foreach (var platform in _platforms) {
                var particle = Instantiate(destroyParticle, platform.transform.position, Quaternion.identity);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(platform);
            }
            
            _platforms.Clear();
        }
    }
}