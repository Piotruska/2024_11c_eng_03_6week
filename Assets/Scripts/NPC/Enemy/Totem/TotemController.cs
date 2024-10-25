using System;
using System.Collections;
using NPC.Enemy.Interfaces;
using UnityEngine;

namespace NPC.Enemy.Totem
{
    public class TotemController : MonoBehaviour
    {
        private ITotemAnimation _totemAnimationController;
        [SerializeField] private GameObject _woodSpike_Projectile;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Collider2D _hitRange;
        [SerializeField] private float _cooldown = 2;
        private bool _playerInRange;
        
        private void Awake()
        {
            _totemAnimationController = GetComponent<TotemAnimationController>();
        }

        public void SetPlayerInRange(bool playerInRange)
        {
            _playerInRange = playerInRange;
        }

        public void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                if (_playerInRange)
                {
                    _totemAnimationController.Shoot();
                }
                yield return new WaitForSeconds(_cooldown);
            }
        }

        public void SpawnProjectile()
        {
            Instantiate(_woodSpike_Projectile, _spawnPoint.position, transform.rotation);
        }
    }
}
