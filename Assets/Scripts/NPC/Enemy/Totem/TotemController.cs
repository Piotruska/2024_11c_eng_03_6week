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
        [SerializeField] private float _cooldown = 2;
        
        void Awake()
        {
            _totemAnimationController = GetComponent<TotemAnimationController>();
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                _totemAnimationController.Shoot();
                //yield return new WaitForSeconds((float)0.12);
                yield return new WaitForSeconds(_cooldown);
            }
        }

        public void SpawnProjectile()
        {
            //used ing animation event for the totem shooting frame
            Instantiate(_woodSpike_Projectile, _spawnPoint.position, transform.rotation);
        }
    }
}
